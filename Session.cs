using System;
using System.Collections.Generic;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, PRODUCT_INFO, SHOPPING_ASSISTANCE, ORDER_TRACKING, SEARCH, HELP_SUPPORT
        }

        Dictionary<string, string> itemHash = new Dictionary<string, string>();
        private State nCur = State.WELCOMING;
        private Order oOrder;

        public Session(string sPhone)
        {
            this.oOrder = new Order();
            //this.oOrder.Phone = sPhone;
            this.itemHash.Add("1", "Product Information");
            this.itemHash.Add("2", "Shopping Assistance");
            this.itemHash.Add("3", "Order Tracking");            this.itemHash.Add("4", "Search for Products");            this.itemHash.Add("5", "Help and Support");
        }

        public List<String> OnMessage(String sInMessage)
        {
            List<String> aMessages = new List<string>();

            if (!isHasmapHashThis(this.itemHash, sInMessage.Trim()) && this.nCur == State.PRODUCT_INFO)
            {
                this.nCur = State.WELCOMING;
                aMessages.Add("Please Enter a Valid Option.");
            }

            switch (this.nCur)
            {
                case State.WELCOMING:
                    aMessages.Add("Hi! Welcome to our E-commerce Chatbot. How may I assist you today?");
                    aMessages.Add("1. Product Information");
                    aMessages.Add("2. Shopping Assistance");
                    aMessages.Add("3. Order Tracking");
                    aMessages.Add("4. Search for Products");
                    aMessages.Add("5. Help and Support");
                    this.nCur = State.PRODUCT_INFO;
                    break;

                case State.PRODUCT_INFO:
                    this.oOrder.Welcome = sInMessage;
                    // Handle product information logic
                    aMessages.Add("Sure! I can provide information about "+this.itemHash[this.oOrder.Welcome.Trim()]+". What product are you interested in?");
                    this.nCur = State.SHOPPING_ASSISTANCE;
                    break;

                case State.SHOPPING_ASSISTANCE:
                    this.oOrder.ProductInfo = sInMessage;
                    // Handle shopping assistance logic
                    aMessages.Add("Great! How can I assist you with your shopping today?");
                    this.nCur = State.ORDER_TRACKING;
                    break;

                case State.ORDER_TRACKING:
                    this.oOrder.ShippingAssistants = sInMessage;
                    // Handle order tracking logic
                    aMessages.Add("Certainly! Please provide your order details for tracking.");
                    oOrder.Save();
                    this.nCur = State.SEARCH;
                    break;

                case State.SEARCH:
                    // Handle search logic
                    this.oOrder.OrderTracking = sInMessage;
                    aMessages.Add("Sure! What product are you looking for? I can help you find it.");
                    this.nCur = State.HELP_SUPPORT;
                    break;

                case State.HELP_SUPPORT:
                    this.oOrder.Search = sInMessage;
                    // Handle help and support logic
                    oOrder.Save();
                    aMessages.Add("If you have any questions or need assistance, feel free to ask! Thanks you");
                    this.nCur = State.HELP_SUPPORT;
                    break;
            }

            aMessages.ForEach(delegate (String sMessage)
            {
                System.Diagnostics.Debug.WriteLine(sMessage);
            });

            return aMessages;
        }

        private string ExtractOrderDetails(string message)
        {
            // Implement logic to extract order details from the message
            // For example, you might use regular expressions to find order numbers.
            // Return the extracted order details.
            return "Order #123456"; // Placeholder, replace with actual implementation
        }

        private string GetOrderSize(string orderDetails)
        {
            // Implement logic to get order size based on order details
            // For example, you might query a database to get the size associated with the order.
            // Return the order size.
            return "Large"; // Placeholder, replace with actual implementation
        }

        bool isHasmapHashThis(Dictionary<string, string> itemHash, string v)
        {

            if (itemHash.ContainsKey(v))
            {
                return true;
            }

            return false;
        }

    }

}
