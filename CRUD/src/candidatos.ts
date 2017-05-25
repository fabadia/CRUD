import { HttpClient } from 'aurelia-fetch-client';
import { autoinject } from 'aurelia-framework';
import { DialogService, DialogOpenResult } from "aurelia-dialog";
import { Candidato } from "./candidato";
import { MensageBoxResult, MensageBox } from "./resources/mensagebox";

@autoinject
export class Candidatos {
    public candidatos = [];

    constructor(private http: HttpClient, private dialog: DialogService) {
        http.configure(config => {
            config
                .useStandardConfiguration()
                .withBaseUrl('/api/Candidatos');
        });
    }

    activate() {
        return this.http.fetch("")
            .then(response => response.json())
            .then(candidatos => this.candidatos = <any>candidatos);
    }

    incluir() {
        let candidato = <any>{};
        this.editar(candidato)
            .then(result => {
                if (result)
                    this.candidatos.push(candidato);
            });
    }

    editar(candidato): Promise<boolean> {
        return this.dialog.open({ viewModel: Candidato, model: candidato.id, keyboard: ['Enter', 'Escape'] })
            .then(result => {
                return (<DialogOpenResult>result).closeResult;
            })
            .then((dialogResult) => {
                if (!dialogResult.wasCancelled && dialogResult.output) {
                    candidato.id = dialogResult.output.id;
                    candidato.nome = dialogResult.output.nome;
                    candidato.eMail = dialogResult.output.eMail;
                    candidato.skype = dialogResult.output.skype;
                    candidato.linkedIn = dialogResult.output.linkedIn;
                    return true;
                }
                return false;
            });
    }

    excluir(candidato) {
        MensageBox.confirm("Deseja realmente excluir?")
            .then((result) => {
                if (result === MensageBoxResult.yes) {
                    this.http.fetch('/' + candidato.id, { method: 'DELETE' })
                        .then(value => {
                            let index = this.candidatos.indexOf(candidato);
                            if (index !== -1)
                                this.candidatos.splice(index, 1);
                            MensageBox.showInfo('Exclusão efetuada com sucesso');
                        })
                        .catch(reason => {
                            MensageBox.showWarning('Exclusão não efetuada. (' + reason.status + '-' + reason.statusText + ')');
                        });
                }
            });
    }
}
