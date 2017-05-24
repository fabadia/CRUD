import { bindable, bindingMode, inject, DOM } from "aurelia-framework";

@inject(Element)
export class CheckboxSet {
    @bindable() itemId: string = 'id';
    @bindable() itemLabel: string = 'descricao';
    @bindable() items: Array<any>;
    @bindable({ defaultBindingMode: bindingMode.twoWay }) values: Array<any>;
    @bindable() valueId: string;
    @bindable() name: string;

    constructor(private element: Element) {
        (<any>element).focus = () => element.querySelector('input').focus();
    }

    bind() {
        this.loadValues();
    }

    blur() {
        setTimeout(() => {
            if (!this.element.querySelector(':focus')) {
                const event = DOM.createCustomEvent('blur', { bubbles: false, cancelable: false });
                this.element.dispatchEvent(event);
            }
        });
    }

    itemsChanged() {
        this.loadValues();
    }

    valuesChanged() {
        this.loadValues();
    }

    loadValues() {
        if (!this.values || !this.items)
            return;

        let lookup = {};
        for (let value of this.values)
            lookup[value[this.valueId]] = value;

        for (let i = this.items.length - 1; i >= 0; i--) {
            this.items[i].checked = !!lookup[this.items[i][this.itemId]];
        }
    }

    update(item) {
        if (item.checked)
            (<any>this.values).addNew(item[this.itemId]);
        else
            for (let i = this.values.length - 1; i >= 0; i--)
                if (this.values[i][this.valueId] === item[this.itemId]) {
                    this.values.splice(i, 1);
                    break;
                }
    }
}