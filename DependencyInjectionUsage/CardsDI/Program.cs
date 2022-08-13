using System;


namespace FactoryDesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                Console.WriteLine("Platinum Card");
                Cards("Platinum");

                Console.WriteLine("Titanium Card");
                Cards("Titanium");

                Console.WriteLine("MoneyBack Card");
                Cards("MoneyBack");
            }
        }

        private static void Cards(string cardType)
        {
            ICreditCard cardDetails = CreditCardFactory.GetCreditCard(cardType);

            if (cardDetails != null)
            {
                Console.WriteLine("CardType : " + cardDetails.GetCardType());
                Console.WriteLine("CreditLimit : " + cardDetails.GetCreditLimit());
                Console.WriteLine("AnnualCharge :" + cardDetails.GetAnnualCharge());
            }
            else
            {
                Console.Write("Invalid Card Type");
            }
        }
    }
}
