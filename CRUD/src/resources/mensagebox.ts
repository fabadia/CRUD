import { bindable, autoinject } from 'aurelia-framework'
import { DialogController, DialogService } from 'aurelia-dialog'

@autoinject
export class MensageBox {
    private static service: DialogService
    private static timeout: number;

    static initialize(service: DialogService, timeout?: number): void {
        this.service = service;
        this.timeout = timeout;
    }

    constructor(private controller: DialogController) {

    }

    private title: string;
    private mensage: string;
    private type: string;
    private buttons: { caption: string, value: MensageBoxResult }[];
    private timeout: number;

    activate(dialogConfig: IDialogConfig) {
        this.title = dialogConfig.title;
        this.mensage = dialogConfig.mensage;

        switch (dialogConfig.type) {
            case MensageBoxType.confirmation:
                this.type = 'primary';
                break;
            case MensageBoxType.information:
                this.type = 'info';
                break;
            case MensageBoxType.warning:
                this.type = 'warning';
                break;
            case MensageBoxType.alert:
            case MensageBoxType.error:
                this.type = 'danger';
                break;
            default:
        }

        this.buttons = [];
        if (dialogConfig.buttons == MensageBoxButtons.ok || dialogConfig.buttons === MensageBoxButtons.okCancel) {
            this.buttons.push({ caption: 'OK', value: MensageBoxResult.ok });
        }
        if (dialogConfig.buttons == MensageBoxButtons.yesNo || dialogConfig.buttons === MensageBoxButtons.yesNoCancel) {
            this.buttons.push({ caption: 'Sim', value: MensageBoxResult.yes });
            this.buttons.push({ caption: 'Não', value: MensageBoxResult.no });
        }
        if (dialogConfig.buttons == MensageBoxButtons.okCancel || dialogConfig.buttons === MensageBoxButtons.yesNoCancel) {
            this.buttons.push({ caption: 'Cancelar', value: MensageBoxResult.cancel });
        }

        this.timeout = dialogConfig.timeout;
    }

    attached() {
        if (this.buttons.length === 1 && this.timeout > 0)
            setTimeout(() => this.controller.ok(this.buttons[0].value), this.timeout);
    }

    private click(result) {
        if (result === MensageBoxResult.cancel)
            this.controller.cancel(result);
        else
            this.controller.ok(result);
    }

    public static showDialog(dialogConfig: IDialogConfig): Promise<MensageBoxResult> {
        if (dialogConfig.timeout === undefined)
            dialogConfig.timeout = this.timeout;
        return this.service.open({ viewModel: MensageBox, model: dialogConfig, keyboard: ['Enter', 'Escape'] })
            .whenClosed((result) => {
                return <MensageBoxResult>result.output;
            });
    }

    public static confirm(mensage: string): Promise<MensageBoxResult> {
        return this.showDialog({ title: 'Confirmação', mensage: mensage, type: MensageBoxType.confirmation, buttons: MensageBoxButtons.yesNo });
    }

    public static showAlert(mensage: string, timeout?: number): Promise<MensageBoxResult> {
        return this.showDialog({ title: 'Alerta', mensage: mensage, type: MensageBoxType.alert, buttons: MensageBoxButtons.ok, timeout: timeout });
    }

    public static showWarning(mensage: string, timeout?: number): Promise<MensageBoxResult> {
        return this.showDialog({ title: 'Aviso', mensage: mensage, type: MensageBoxType.warning, buttons: MensageBoxButtons.ok, timeout: timeout });
    }

    public static showInfo(mensage: string, timeout?: number): Promise<MensageBoxResult> {
        return this.showDialog({ title: 'Informação', mensage: mensage, type: MensageBoxType.information, buttons: MensageBoxButtons.ok, timeout: timeout });
    }

    public static showError(mensage: string, timeout?: number): Promise<MensageBoxResult> {
        return this.showDialog({ title: 'Erro', mensage: mensage, type: MensageBoxType.error, buttons: MensageBoxButtons.ok, timeout: timeout });
    }
}

export function confirm(mensage: string): Promise<MensageBoxResult> {
    return MensageBox.confirm(mensage);
}

export function showAlert(mensage: string): Promise<MensageBoxResult> {
    return MensageBox.showAlert(mensage);
}

export function showWarning(mensage: string): Promise<MensageBoxResult> {
    return MensageBox.showWarning(mensage);}

export function showInfo(mensage: string): Promise<MensageBoxResult> {
    return MensageBox.showInfo(mensage);}

export function showError(mensage: string): Promise<MensageBoxResult> {
    return MensageBox.showError(mensage);}

interface IDialogConfig {
    title: string;
    mensage: string;
    type: MensageBoxType;
    buttons: MensageBoxButtons,
    timeout?: number;
}

export enum MensageBoxType {
    information,
    confirmation,
    alert,
    warning,
    error
}

export enum MensageBoxButtons {
    ok = 1,
    okCancel = 9,
    yesNo = 6,
    yesNoCancel = 14
}

export enum MensageBoxResult {
    none = 0,
    ok = 1,
    yes = 2,
    no = 4,
    cancel = 8,
}