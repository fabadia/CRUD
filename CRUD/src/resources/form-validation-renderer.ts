import {
    ValidationRenderer,
    RenderInstruction,
    ValidateResult
} from 'aurelia-validation';

export class FormValidationRenderer {
    render(instruction: RenderInstruction) {
        for (let { result, elements } of instruction.unrender) {
            for (let element of elements) {
                this.remove(element, result);
            }
        }

        for (let { result, elements } of instruction.render) {
            for (let element of elements) {
                this.add(element, result);
            }
        }
    }

    add(element: Element, result: ValidateResult) {
        if (result.valid) {
            return;
        }

        const formGroup = element.closest('.form-group');
        if (!formGroup) {
            return;
        }

        // add the has-error class to the enclosing form-group div
        formGroup.classList.add('has-error');

        const helpBlock = formGroup.getElementsByClassName('help-block')[0];

        // add help-block
        const message = document.createElement('li');
        message.className = 'validation-message';
        message.textContent = result.message;
        message.id = `validation-message-${result.id}`;
        helpBlock.appendChild(message);
    }

    remove(element: Element, result: ValidateResult) {
        if (result.valid) {
            return;
        }

        const formGroup = element.closest('.form-group');
        if (!formGroup) {
            return;
        }

        const helpBlock = formGroup.getElementsByClassName('help-block')[0];

        // remove help-block
        const message = formGroup.querySelector(`#validation-message-${result.id}`);
        if (message) {
            message.parentElement.removeChild(message);

            // remove the has-error class from the enclosing form-group div
            if (helpBlock.children.length === 0) {
                formGroup.classList.remove('has-error');
            }
        }
    }
}