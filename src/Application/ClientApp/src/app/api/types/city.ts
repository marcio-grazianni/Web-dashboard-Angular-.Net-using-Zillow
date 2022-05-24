export class City {
    id: number;
    name: string;
    code: string;
    marketName: string;
    marketId: number;
    totalListings: number;

    totalOccupancyRequests: number = 0;
    totalADRRequests: number = 0;
    totalRevenueRequests: number = 0;

    get needsAirDNAId(): boolean {
        return this.marketId === 0;
    }

    get totalCost(): number {
        return (this.totalADRRequests * 0.5) + (this.totalOccupancyRequests * 0.5) + (this.totalRevenueRequests) * 1
    }

    constructor(data: any) {
        if (!data) { return; }
        for (let property in data) {
            if (!data.hasOwnProperty(property)) { continue; }
            (<any>this)[property] = data[property];
        }        
    }

    static fromJS(data: any): City {
        data = typeof data === 'object' ? data : {};
        return new City(data);
    }

    static fromJSCollection(data: any[]): City[] {
        if (!data) return null;
        return data.map(d => City.fromJS(d));
    }
}