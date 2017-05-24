import { bindable, bindingMode, inject, DOM } from "aurelia-framework";

@inject(Element)
export class RadioSet {
    @bindable() optionId: string = 'id';
    @bindable() optionLabel: string = 'descricao';
    @bindable() options: Array<any>;
    @bindable({ defaultBindingMode: bindingMode.twoWay }) value;
    @bindable() name: string;

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