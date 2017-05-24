import { HttpClient } from 'aurelia-fetch-client';
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

    open() {
        this.dialog.open({ viewModel: Candidato, model: null });
    }
}
