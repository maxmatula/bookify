import BookingResponse from './booking.model';

export default class ClientResponse {
    public ClientId: number | undefined;
    public Name: string | undefined;
    public Lastname: string | undefined;
    public Email: string | undefined;
    public Phone: string | undefined;

    public Bookings: BookingResponse[] | undefined;
}
