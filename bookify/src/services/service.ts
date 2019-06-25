import { Http } from '../http';

export default class Service {
    public async get(url: string) {
       return new Promise((resolve, reject) => {
           Http.get(url)
           .then((response) => {
                resolve(response.data);
           })
           .catch((error) => {
                reject(error);
           });
       });
    }

    public async delete(url: string) {
        return new Promise((resolve, reject) => {
            Http.delete(url)
            .then((response) => {
                 resolve(response.data);
            })
            .catch((error) => {
                 reject(error);
            });
        });
     }

    public async post(url: string, data: object) {
       return new Promise((resolve, reject) => {
           Http.post(url, data,
            {
                headers: {'Content-Type': 'application/json' },
            })
           .then((response) => {
                resolve(response.data);
           })
           .catch((error) => {
                reject(error);
           });
       });
    }
}
