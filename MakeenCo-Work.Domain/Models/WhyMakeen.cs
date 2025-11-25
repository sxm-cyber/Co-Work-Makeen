namespace MakeenCo_Work.Domain.Models
{
    public class WhyMakeen : Image
    {
        public bool NetworkOption { get; private set; } = false;

        public bool CafeOption { get; private set; } = false;

        public bool ReasonablePriceOption { get; private set; } = false;

        public bool LockerOption { get; set; } = false;

        public bool PrinterOption { get; private set; } = false;

        public bool EasyAccessOption { get; private set; } = false;

        public bool FridgeOption { get; private set; } = false;

        public bool GameOption { get; set; } = false;

        public bool HeaterOption { get; set; } = false;

        public DateTime UpdateAt { get; private set; }


        public WhyMakeen() { }
        
            
        
        public WhyMakeen(bool networkOption , bool cafeOption, bool reasonablePriceOption , bool lockerOption ,
            bool printerOption , bool easyAccessOption , bool fridgeOption, bool gameOption, bool heaterOption)
        {
            NetworkOption = networkOption;
            CafeOption = cafeOption;
            ReasonablePriceOption = reasonablePriceOption;
            LockerOption = lockerOption;
            PrinterOption = printerOption;
            EasyAccessOption = easyAccessOption;
            FridgeOption = fridgeOption;
            GameOption = gameOption;
            HeaterOption = heaterOption;
        }


        //public void Update(bool networkOption , bool cafeOption , bool reasonablePriceOption, bool lockerOption,
        //    bool printerOption, bool easyAccessOption, bool fridgeOption, bool gameOption, bool heaterOption)
        //{
        //    NetworkOption = networkOption;
        //    CafeOption = cafeOption;
        //    ReasonablePriceOption = reasonablePriceOption;
        //    LockerOption = lockerOption;
        //    PrinterOption = printerOption;
        //    EasyAccessOption = easyAccessOption;
        //    FridgeOption = fridgeOption;
        //    GameOption = gameOption;
        //    HeaterOption = heaterOption;
        //    UpdateTimestamp();
        //}
    }
}
