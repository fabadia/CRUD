import { autoinject, NewInstance, BindingEngine, Disposable } from "aurelia-framework";
import { DialogController } from "aurelia-dialog";
import { HttpClient, json } from "aurelia-fetch-client";
import { PageNavigator } from "./resources/elements/page-navigator"
import { ValidationController, ValidationRules, ValidationControllerFactory, validateTrigger, FluentRules, FluentRuleCustomizer, Rules, validationMessages } from "aurelia-validation";
import { FormValidationRenderer } from "./resources/form-validation-renderer";
import { MensageBoxResult, MensageBox } from "./resources/mensagebox";
import { ChangesChecker } from "./resources/changes-checker";

@autoinject
export class Candidato {
    private candidato;
    private operacao: string;
    private disponibilidades;
    private melhoresHorarios;
    private camposNivel: { nome: string, titulo: string }[];
    private pagination: PageNavigator;
    private subscriptionPaginationChange: Disposable;
    private validationController: ValidationController = null;
    private metadata;
    private hasRequiredField: boolean;
    private saving: boolean;
    private closingConfirmed: boolean = false;
    private changesChecker: ChangesChecker;

    constructor(private element: Element,
        private http: HttpClient,
        private controller: DialogController,
        private controllerFactory: ValidationControllerFactory,
        private bindingEngine: BindingEngine) {
        this.validationController = controllerFactory.createForCurrentScope();
        this.validationController.validateTrigger = validateTrigger.change;
        this.validationController.addRenderer(new FormValidationRenderer());
        controller.settings.centerHorizontalOnly = true;
    }

    activate(id) {
        let http = new HttpClient();
        http.configure(config => {
            config
                .useStandardConfiguration()
                .withBaseUrl('/api/');
        });
        let promises = [];
        promises.push(http.fetch('Disponibilidades').then(response => response.json()).then(disponibilidades => this.disponibilidades = disponibilidades));
        promises.push(http.fetch('MelhoresHorarios').then(response => response.json()).then(melhoresHorarios => this.melhoresHorarios = melhoresHorarios));
        promises.push(http.fetch('Candidatos/metadata').then(response => response.json()).then(metadata => {
            this.camposNivel = [];
            for (var m in metadata)
                if (m.startsWith('nivel'))
                    this.camposNivel.push({ nome: m, titulo: metadata[m].titulo });
            this.metadata = metadata;
        }));

        if (id) {
            promises.push(
                this.http.fetch('/' + id)
                    .then(response => response.json())
                    .then(candidato => {
                        this.candidato = candidato;
                    }));
        }
        else {
            this.candidato = { id: 0, candidatoDisponibilidades: [], candidatoMelhoresHorarios: [] }
        }
        if (!id)
            this.operacao = "Novo";
        else
            this.operacao = "Editar";
        return Promise.all(promises);
    }

    attached() {
        function markRequired(element: Element) {
            if (!element)
                return;
            const formGroup = element.closest('.form-group');
            const label = formGroup.getElementsByClassName('control-label')[0];
            label.classList.add('required');
        }

        this.subscriptionPaginationChange = this.bindingEngine.propertyObserver(this.pagination, 'currentPage').subscribe(() => this.checkFocus());

        this.candidato.candidatoDisponibilidades.addNew = (id) => {
            this.candidato.candidatoDisponibilidades.push({ "candidatoId": this.candidato.id, "disponibilidadeId": id });
        }

        this.candidato.candidatoMelhoresHorarios.addNew = (id) => {
            this.candidato.candidatoMelhoresHorarios.push({ "candidatoId": this.candidato.id, "melhorHorarioId": id });
        }

        let fluentRules: FluentRules<{}, {}>;
        let fluentRuleCustomizer: FluentRuleCustomizer<{}, {}>;
        for (let m in this.metadata) {
            let element = document.querySelector("[value\\.bind^='candidato." + m + "'], [values\\.bind^='candidato." + m + "'], [name='" + m + "']");
            let page = '';
            if (element) {
                page = this.getPageFor(element).toString();
            }
            let validacao = this.metadata[m].validacao;
            if (Object.keys(validacao).length) {
                if (!fluentRuleCustomizer)
                    fluentRules = ValidationRules.ensure(m);
                else
                    fluentRules = fluentRuleCustomizer.ensure(m);
                fluentRules.displayName(this.metadata[m].titulo);
                if (validacao.required) {
                    fluentRuleCustomizer = fluentRules.required().tag(page);
                    markRequired(element);
                }
                if (validacao.email)
                    fluentRuleCustomizer = fluentRules.email().withMessageKey('default').tag(page);
                if (validacao.maxLength)
                    fluentRuleCustomizer = fluentRules.maxLength(validacao.maxLength).tag(page);
                if (validacao.minLength)
                    fluentRuleCustomizer = fluentRules.minLength(validacao.minLength).tag(page);
                if (validacao.minItems) {
                    fluentRuleCustomizer = fluentRules.minItems(validacao.minItems).withMessageKey('required').tag(page);
                    markRequired(element);
                }
            }
        }
        fluentRuleCustomizer.on(this.candidato);

        validationMessages['default'] = '\${$displayName} não tem um valor válido.';
        validationMessages['required'] = '\${$displayName} deve ser preenchido.';
        validationMessages['maxLength'] = '${$displayName} deve ter no máximo ${$config.length} caracter${$config.length === 1 ? "" : "es"}.';
        validationMessages['minLength'] = '${$displayName} deve ter no mínimo ${$config.length} caracter${$config.length === 1 ? "" : "es"}.';

        this.changesChecker = new ChangesChecker(this.candidato);
    }

    detached() {
        this.subscriptionPaginationChange.dispose();
    }

    canDeactivate() {
        if (!this.closingConfirmed && this.changesChecker.hasChanges()) {
            MensageBox.confirm('As alterações serão perdidas. Deseja realmente fechar?')
                .then(result => {
                    if (result === MensageBoxResult.yes) {
                        this.closingConfirmed = true;
                        this.controller.cancel();
                    }
                });
            return false;
        }
        return true;
    }

    private get isEditing() {
        return !!this.candidato.id;
    }

    checkFocus() {
        let pageElement = this.element.querySelector(':not(.aurelia-hide)[show\\.bind^="pagination"]');
        this.hasRequiredField = pageElement.querySelector('.control-label.required') != null;
        let errorElement = pageElement.querySelector('.has-error');
        let input = <HTMLElement>(errorElement || pageElement).querySelector('input');
        input.focus();
    }

    canGoToNextPage(): Promise<boolean> {
        if (this.isEditing)
            return Promise.resolve(true);
        return this.validationController.validate({ object: this.candidato, rules: ValidationRules.taggedRules(Rules.get(this.candidato), this.pagination.currentPage.toString()) })
            .then(c => {
                if (!c.valid) {
                    this.checkFocus();
                }
                return c.valid;
            });
    }

    getPageFor(element): number {
        let pageElement = element.closest('[show\\.bind^="pagination"]');
        return parseInt(pageElement.attributes['show.bind'].value.match(/\d+/g)[0]);
    }

    checkPageAndfocus() {
        let page = this.getPageFor(this.element.querySelector('.has-error'));
        if (page !== this.pagination.currentPage)
            this.pagination.goToPage(page);
        else
            this.checkFocus();
    }

    save() {
        this.saving = true;
        this.validationController.validate()
            .then(c => {
                if (!c.valid) {
                    this.saving = false;
                    this.checkPageAndfocus();
                }
                else {
                    let save: Promise<any>;
                    if (this.candidato.id)
                        save = this.http.fetch('/' + this.candidato.id, { method: 'put', body: json(this.candidato) });
                    else
                        save = this.http.fetch('', { method: 'post', body: json(this.candidato) })
                            .then(response => <any>response.json())
                            .then(candidato => this.candidato.id = candidato.id);

                    save
                        .then(() => {
                            this.closingConfirmed = true;
                            this.controller.ok(this.candidato);
                        })
                        .catch((reason: Response) => {
                            this.saving = false;
                            if (reason.bodyUsed)
                                reason.json().then(value => {
                                    for (let n in value) {
                                        this.validationController.addError(value[n][0], this.candidato, n);
                                    }
                                    this.checkPageAndfocus();
                                });
                            else
                                MensageBox.showError(`Erro ao salvar. (${reason.status} - ${reason.statusText})`);
                        });
                }
            });
    }
}