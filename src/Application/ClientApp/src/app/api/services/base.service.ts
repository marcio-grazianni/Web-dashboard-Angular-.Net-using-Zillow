

export class BaseService {

    private serviceUrl: string;
    
    private get baseUrl(): string {
        return "/api";
    }

    protected get apiUrl(): string {
        return `${this.baseUrl}${this.serviceUrl}`;
    }

    constructor(serviceUrl: string) { 
        this.serviceUrl = serviceUrl;
    }
}