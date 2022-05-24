export class JobRequest {
    type: JobType;
    title: string;
    runAt?: Date;

    constructor(data: any) {
        if (!data) { return; }
        for (let property in data) {
            if (!data.hasOwnProperty(property)) { continue; }
            (<any>this)[property] = data[property];
        }
    }
}

export enum JobType {
    ListingDiscovery,
    MarketHistoryDiscovery,
    Calculation,
    MarketSummaryDiscovery
}