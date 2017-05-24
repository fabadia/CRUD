import { bindable, bindingMode, inject, DOM } from "aurelia-framework";

@inject(Element)
export class RadioLevel {
    @bindable() levels = 6;
    @bindable({ defaultBindingMode: bindingMode.twoWay }) value;
    @bindable() name: string;

    constructor(private element: Element) {
        (<any>element).focus = () => element.querySelector('input').focus();
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