export class MarketHistory {

    pricing: HistoryRecord[];
    occupancy: HistoryRecord[];
    revenue: HistoryRecord[];
    summary: HistoryRecord[];

    constructor(data: any) {
        if (!data) { return; }
        for (let property in data) {
            if (!data.hasOwnProperty(property)) { continue; }
            (<any>this)[property] = data[property];
        }
        if (this.occupancy) {
            this.occupancy.forEach(o => {
                o.percentiles = o.percentiles.map(p => p * 100);
            });
        }
    }

    public getPricingByBedrooms(bedrooms: number): HistoryRecord[] {
        return this.pricing.filter(p => p.bedrooms === bedrooms);
    }

    public getOccupancyByBedrooms(bedrooms: number): HistoryRecord[] {
        return this.occupancy.filter(p => p.bedrooms === bedrooms);
    }

    public getRevenueByBedrooms(bedrooms: number): HistoryRecord[] {
        return this.revenue.filter(p => p.bedrooms === bedrooms);
    }

    public getAverageMonthlyRevenueByBedrooms(bedrooms: number): number[] {
        let pricing = this.getPricingByBedrooms(bedrooms);
        let occupancy = this.getOccupancyByBedrooms(bedrooms);
        let data: number[] = [];
        let days: number[] = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
        occupancy.forEach((o, i) => {
            let lowRevenue = pricing[i].percentiles[1] * (o.percentiles[1] * 0.01);
            let middleRevenue = pricing[i].percentiles[1] * (o.percentiles[2] * 0.01);
            let highRevenue = pricing[i].percentiles[1] * (o.percentiles[3] * 0.01);
            let avg =  days[i] * (lowRevenue + middleRevenue + highRevenue) / 3;
            data.push(avg);
        });
        return data;
    }

    public getMonthlyRevenueByBedrooms(bedrooms: number, index: number): number[] {
        let revenue = this.getRevenueByBedrooms(bedrooms);
        let data: number[] = [];
        revenue.forEach(r => data.push(r.percentiles[index]));
        return data;
    }

    public getYearlyRevenueByBedrooms(bedrooms: number, index: number): number {
        let total = 0;
        let revenue = this.getMonthlyRevenueByBedrooms(bedrooms, index);
        revenue.forEach(r => total += r);
        return total;
    }

    public getLatestSummary(): any {
        let summary = null;
        let latest = this.summary && this.summary.length ? this.summary[this.summary.length - 1] : null;
        if(latest) summary = JSON.parse(latest.data);
        return summary;
    }

    static fromJS(data: any): MarketHistory {
        data = typeof data === 'object' ? data : {};
        return new MarketHistory(data);
    }

    static fromJSCollection(data: any[]): MarketHistory[] {
        if (!data) return null;
        return data.map(d => MarketHistory.fromJS(d));
    }
}

export interface HistoryRecord {
    bedrooms: number;
    month: number;
    percentiles: number[];
    data: string;
}