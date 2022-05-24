import { City } from "./city";

export class State {
    name: string;
    cities: City[] = [];
    validCities: City[] = [];
    totalCities: number = 0;
    totalListings: number = 0;

    get slug(): string {
        return this.name.toLocaleLowerCase();
    }

    constructor(data: any) {
        if (!data) { return; }
        for (let property in data) {
            if (!data.hasOwnProperty(property)) { continue; }
            switch (property) {
                case 'cities':
                    this.cities = City.fromJSCollection(data[property]);
                    this.validCities = this.cities.filter(m => !!m.code);
                    break;
                default:
                    (<any>this)[property] = data[property];
                    break;
            }
        }
    }

    static fromJS(data: any): State {
        data = typeof data === 'object' ? data : {};
        return new State(data);
    }

    static fromJSCollection(data: any[]): State[] {
        if (!data) return null;
        return data.map(d => State.fromJS(d));
    }
}