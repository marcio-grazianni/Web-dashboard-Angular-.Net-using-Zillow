export class Listing {
    id: number;
    liked: boolean;
    zId: number;
    daysOnMarket: number;
    status: string;
    price: number;
    tax: number;
    estimatedTax: number;
    prediction: any;
    capRate: any;
    address: any;
    description: any;

    details: any;
    createdAt: string;

    get lowCapRate(): number {
        return this.capRate.low * 100;
    }

    get middleCapRate(): number {
        return this.capRate.middle * 100;
    }

    get highCapRate(): number {
        return this.capRate.high * 100;
    }

    get longTermRate(): number {
        return this.capRate.longTerm * 100;
    }

    get isForSale(): boolean {
        return this.status === 'FOR_SALE';
    }


    get coverPicture(): string {
        return this.details ? this.details.photos[0] : ''
    }

    get profilePicture(): string {
        return this.details ? this.details.photos[1] : ''
    }

    get predictedRevenue(): number {
        return this.prediction ? this.prediction.revenue : 0;
    }

    get predVsPrice(): number {
        return this.prediction ? this.prediction.predVsPrice * 100 : 0;
    }

    constructor(data: any) {
        if (!data) { return; }
        for (let property in data) {
            if (!data.hasOwnProperty(property)) { continue; }
            (<any>this)[property] = data[property];
        }
    }

    static fromJS(data: any): Listing {
        data = typeof data === 'object' ? data : {};
        return new Listing(data);
    }

    static fromJSCollection(data: any[]): Listing[] {
        if (!data) return null;
        return data.map(d => Listing.fromJS(d));
    }
}