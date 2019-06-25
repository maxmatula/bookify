export default class BookingResponse {
    public BookingId: number | undefined;
    public BookingDate: Date | undefined;
    public DateFrom: Date | undefined;
    public DateTo: Date | undefined;
    public Adults: number | undefined;
    public Kids: number | undefined;
    public Animals: number | undefined;
    public AdvancePaid: boolean | undefined;
    public IsPaid: boolean | undefined;

    public HouseId: number | null | undefined;
    public ClientId: number | null | undefined;
}
