import ClientResponse from './client.model';
export default class BookingResponse {
    public bookingId: number | undefined;
    public bookingDate: string | undefined;
    public dateFrom: string | undefined;
    public dateTo: string | undefined;
    public adults: string | undefined;
    public kids: string | undefined;
    public animals: string | undefined;
    public advancePaid: boolean | undefined;
    public isPaid: boolean | undefined;
    public client: ClientResponse | undefined;

    public houseId: number | null | undefined;
    public clientId: number | null | undefined;
}
