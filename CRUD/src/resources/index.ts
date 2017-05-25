import { FrameworkConfiguration } from 'aurelia-framework';
import { MensageBox } from './mensagebox';
import { DialogService } from 'aurelia-dialog';

export function configure(config: FrameworkConfiguration) {
    config.globalResources([
        "./elements/page-navigator",
        "./elements/checkbox-set",
        "./elements/radio-level",
        "./elements/radio-set"
    ]);
    MensageBox.initialize(config.container.get(DialogService), 3000);
}
