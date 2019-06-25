import AddressResponse from './address.model';
import EquipmentResponse from './equipment.model';
import BookingResponse from './booking.model';

export default class HouseResponse {
    public HouseId: number | undefined;
    public Title: string | undefined;
    public Description: string | undefined;
    public Rooms: number | undefined;
    public Bathrooms: number | undefined;
    public Floors: number | undefined;
    public Price: number | undefined;

    public AddressId: number | undefined;
    public Address: AddressResponse | undefined;
    public Equipment: EquipmentResponse[] | undefined;
    public Bookings: BookingResponse[] | undefined;
}