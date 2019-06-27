import Service from './service';
export default class HomeService extends Service {
    public countries = [
        { value: 'Polska', text: 'Polska' },
    ];
    public floors = [
        { value: null, text: 'Wybierz ilość pięter' },
        { value: 0, text: 'Budynek parterowy' },
        { value: 1, text: '1' },
        { value: 2, text: '2' },
        { value: 3, text: '3' },
    ];

    public rooms = [
        { value: null, text: 'Wybierz ilość pokoi' },
        { value: 1, text: '1' },
        { value: 2, text: '2' },
        { value: 3, text: '3' },
        { value: 4, text: '4' },
    ];
    public bathrooms = [
        { value: null, text: 'Wybierz ilość pięter' },
        { value: 0, text: 'Brak' },
        { value: 1, text: '1' },
        { value: 2, text: '2' },
    ];
}
