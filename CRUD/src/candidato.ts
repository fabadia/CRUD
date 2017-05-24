import { autoinject, NewInstance, BindingEngine, Disposable } from "aurelia-framework";
import { DialogController } from "aurelia-dialog";
import { HttpClient, json } from "aurelia-fetch-client";
import { PageNavigator } from "./resources/elements/page-navigator"

@autoinject
export class Candidato {
    private candidato;
    private operacao: string;
    private disponibilidades;
    private melhoresHorarios;
    private camposNivel: { nome: string, titulo: string }[];
    private pagination: PageNavigator;
    private subscriptionPaginationChange: Disposable;

    constructor(private element: Element,
        private controller: DialogController,
        private bindingEngine: BindingEngine) {
        controller.settings.centerHorizontalOnly = true;
    }

    activate() {
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
        }));

        this.candidato = { id: 0, candidatoDisponibilidades: [], candidatoMelhoresHorarios: [] }

        return Promise.all(promises);
    }

    attached() {
        this.subscriptionPaginationChange = this.bindingEngine.propertyObserver(this.pagination, 'currentPage').subscribe(() => this.checkFocus());

        this.candidato.candidatoDisponibilidades.addNew = (id) => {
            this.candidato.candidatoDisponibilidades.push({ "candidatoId": this.candidato.id, "disponibilidadeId": id });
        }

        this.candidato.candidatoMelhoresHorarios.addNew = (id) => {
            this.candidato.candidatoMelhoresHorarios.push({ "candidatoId": this.candidato.id, "melhorHorarioId": id });
        }
    }

    detached() {
        this.subscriptionPaginationChange.dispose();
    }

    checkFocus() {
        let pageElement = this.element.querySelector(':not(.aurelia-hide)[show\\.bind^="pagination"]');
        let input = <HTMLElement>(pageElement).querySelector('input');
        input.focus();
    }

    canGoToNextPage(): Promise<boolean> {
        return Promise.resolve(true);
    }
}