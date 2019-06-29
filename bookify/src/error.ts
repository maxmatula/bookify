import { AxiosError } from 'axios';

export default class ErrorFormatter {
    constructor(error: AxiosError) {
        if (error.response === undefined || error.response.status >= 400) {
            alert('Pobranie danych nie było możliwe. Sprawdź swoje połączenie z internetem.');
          } else {
            alert(error.message);
          }
    }
}
