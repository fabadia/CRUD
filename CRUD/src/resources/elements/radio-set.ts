import { bindable, bindingMode, inject, DOM } from "aurelia-framework";
import { validateTrigger } from "aurelia-validation";

@inject(Element)
export class RadioSet {
    @bindable() optionId: string = 'id';
    @bindable() optionLabel: string = 'descricao';
    @bindable() options: Array<any>;
    @bindable({ defaultBindingMode: bindingMode.twoWay }) value;
    @bindable() name: string;
    private binding;

    constructor(private element: Element) {
        (<any>element).focus = () => element.querySelector('input').focus();
    }

    bind() {
        if (!Array.isArray(this.options[0])) {
            for (let i = 0; i < this.options.length; i++) {
                let option = this.options[i];
                this.options[i] = [option[this.optionId], option[this.optionLabel]];
            }
        }

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

    validate() {
        if (this.binding)
            this.binding.validationController.validateBinding(this.binding);
    }
}