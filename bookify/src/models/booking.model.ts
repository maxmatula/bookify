export default class BookingResponse {
    public BookingId: number | undefined;
    public BookingDate: string | undefined;
    public DateFrom: string | undefined;
    public DateTo: string | undefined;
    public Adults: string | undefined;
    public Kids: string | undefined;
    public Animals: string | undefined;
    public AdvancePaid: boolean | undefined;
    public IsPaid: boolean | undefined;

    public HouseId: number | null | undefined;
    public ClientId: number | null | undefined;
}
