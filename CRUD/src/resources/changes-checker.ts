export class ChangesChecker {
    private original: Object;
    constructor(private obj: Object) {
        this.original = JSON.parse(JSON.stringify(obj));
    }

    /*
        Baseado no comentárioe de Ebrahim Byagowi
        em https://stackoverflow.com/questions/201183/how-to-determine-equality-for-two-javascript-objects/16788517
        modificado para considerar functions.
    */
    private isEquals(x, y): boolean {
        // if they are functions, they should exactly refer to same one (because of closures)
        if (x instanceof Function) { return true; }
        if (x === null || x === undefined || y === null || y === undefined) { return x === y; }
        // after this just checking type of one would be enough
        if (x.constructor !== y.constructor) { return false; }
        // if they are regexps, they should exactly refer to same one (it is hard to better equality check on current ES)
        if (x instanceof RegExp) { return x === y; }
        if (x === y || x.valueOf() === y.valueOf()) { return true; }
        if (Array.isArray(x) && x.length !== y.length) { return false; }

        // if they are dates, they must had equal valueOf
        if (x instanceof Date) { return false; }

        // if they are strictly equal, they both need to be object at least
        if (!(x instanceof Object)) { return false; }
        if (!(y instanceof Object)) { return false; }

        // recursive object equality check
        var p = Object.keys(x);
        return p.every(i => { return this.isEquals(x[i], y[i]); });
    }

    public hasChanges(): boolean {
        return !this.isEquals(this.obj, this.original);
    }
}
