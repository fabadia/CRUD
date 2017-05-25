import { bindable, bindingMode, inject, DOM } from "aurelia-framework";
import { validateTrigger } from "aurelia-validation";

@inject(Element)
export class RadioLevel {
    @bindable() levels = 6;
    @bindable({ defaultBindingMode: bindingMode.twoWay }) value;
    @bindable() name: string;
    private binding;

    constructor(private element: Element) {
        (<any>element).focus = () => element.querySelector('input').focus();
    }

    bind() {
        for (let boundProperty of (<any>this.element).au.controller.boundProperties) {
            let binding = boundProperty.binding;
            if (binding.targetProperty === "value" && binding["behavior-validate"].getValidateTrigger(binding.validationController) & validateTrigger.change) {
                this.binding = binding;
                break;
            }
        }
    }

    blur() {
        setTimeout(() => {
            if (!this.element.querySelector(':focus')) {
                const event = DOM.createCustomEvent('blur', { bubbles: false, cancelable: false });
                this.element.dispatchEvent(event);
            }
        });
    }
}