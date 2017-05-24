import { FrameworkConfiguration } from 'aurelia-framework';

export function configure(config: FrameworkConfiguration) {
    config.globalResources([
        "./elements/page-navigator",
        "./elements/checkbox-set",
        "./elements/radio-level",
        "./elements/radio-set"
    ]);
}
