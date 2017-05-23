export class App {
    private router;
    configureRouter(config, router) {
        config.title = 'Aurelia CRUD Demo';
        config.map([
            { route: ['', 'home'], name: 'home', moduleId: 'home', nav: true, title: 'Home' },
            { route: 'candidatos', name: 'candidatos', moduleId: 'candidatos', nav: true, title: 'Candidatos' }
        ]);
        this.router = router;
    }
}
