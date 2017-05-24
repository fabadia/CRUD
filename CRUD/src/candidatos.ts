﻿import { HttpClient } from 'aurelia-fetch-client';
import { autoinject } from 'aurelia-framework';
import { DialogService, DialogOpenResult } from "aurelia-dialog";
import { Candidato } from "./candidato";

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
        this.dialog.open({ viewModel: Candidato, model: candidato.id, keyboard: ['Enter', 'Escape'] })
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
            })
            .then(result => {
                if (result)
                    this.candidatos.push(candidato);
            });
    }
}
