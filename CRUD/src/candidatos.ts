import { HttpClient } from 'aurelia-fetch-client';
import { autoinject } from 'aurelia-framework';

@autoinject
export class Candidatos {
    public candidatos = [];

    constructor(private http: HttpClient) {
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
}
