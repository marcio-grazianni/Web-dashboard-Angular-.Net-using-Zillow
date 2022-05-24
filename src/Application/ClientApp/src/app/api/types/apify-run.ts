export class ApifyRun {
    data: string;
    message: string;
    _data: any;

    get apifyId(): string {
        return this._data.Id;
    }
    get defaultDatasetId(): string {
        return this._data.DefaultDatasetId;
    }
    get startTime(): string {
        return this._data.StartedAt;
    }
    get endTime(): string {
        return this._data.FinishedAt;
    }
    get buildId(): string {
        return this._data.BuildId;
    }
    get status(): number {
        return this.message ? 1 : 0;
    }
    get link(): string {
        return `https://api.apify.com/v2/datasets/${this.defaultDatasetId}/items?clean=true&format=json`;
    }
    
    constructor(data: any) {
        if (!data) { return; }
        for (let property in data) {
            if (!data.hasOwnProperty(property)) { continue; }
            (<any>this)[property] = data[property];
        }  
        this._data = JSON.parse(this.data);    
    }

    static fromJS(data: any): ApifyRun {
        data = typeof data === 'object' ? data : {};
        return new ApifyRun(data);
    }

    static fromJSCollection(data: any[]): ApifyRun[] {
        if (!data) return null;
        return data.map(d => ApifyRun.fromJS(d));
    }
}