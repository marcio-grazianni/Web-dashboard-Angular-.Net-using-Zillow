export class LikeRequest{
    public entityId: number;
    public userId: number = 1;

    constructor(data: any) {
        if (!data) { return; }
        for (let property in data) {
            if (!data.hasOwnProperty(property)) { continue; }
            (<any>this)[property] = data[property];
        }        
    }
}