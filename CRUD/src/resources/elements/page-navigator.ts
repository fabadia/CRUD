import { inject, bindable, computedFrom } from "aurelia-framework";

@inject(Element)
export class PageNavigator {
    @bindable
    readonly totalPages: number;
    @bindable
    private _currentPage: number = 1;
    @bindable
    private canGoToNextPage: () => Promise<boolean>;

    constructor(private element: Element) { }

    @computedFrom('_currentPage')
    public get isFirstPage() {
        return this._currentPage === 1;
    }

    @computedFrom('_currentPage')
    public get isLastPage() {
        return this._currentPage === this.totalPages;
    }

    @computedFrom('_currentPage')
    public get currentPage() {
        return this._currentPage;
    }

    public previousPage() {
        if (!this.isFirstPage)
            this._currentPage--;
    }

    public nextPage() {
        if (!this.isLastPage)
            this.canGoToNextPage()
                .then(c => c && this._currentPage++);
    }

    public goToPage(pageNumber: number): boolean {
        if (pageNumber < 1 || pageNumber > this.totalPages || pageNumber === this._currentPage)
            return false;
        this._currentPage = pageNumber;
        return true;
    }
}
