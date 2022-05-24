export class Job {
    id: number; 
    
    constructor(data: any) {
        if (!data) { return; }
        for (let property in data) {
            if (!data.hasOwnProperty(property)) { continue; }
            (<any>this)[property] = data[property];
        }        
    }

    static fromJS(data: any): Job {
        data = typeof data === 'object' ? data : {};
        return new Job(data);
    }

    static fromJSCollection(data: any[]): Job[] {
        if (!data) return null;
        return data.map(d => Job.fromJS(d));
    }
}