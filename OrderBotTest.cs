using System;
using System.Collections.Generic;
using Xunit;
using OrderBot;

namespace OrderBot.tests
{
    public class SessionTest
    {
        [Fact]
        public void TestWelcome()
        {
            Session session = new Session("12345");
            List<string> messages = session.OnMessage("hello");

            Assert.Contains("Hi! Welcome to our E-commerce Chatbot. How may I assist you today?", messages);
            Assert.Contains("1. Product Information", messages);
            Assert.Contains("2. Shopping Assistance", messages);
            Assert.Contains("3. Order Tracking", messages);
            Assert.Contains("4. Search for Products", messages);
            Assert.Contains("5. Help and Support", messages);
        }

        [Fact]
        public void TestProductInfo()
        {
            Session session = new Session("12345");
            session.OnMessage("hello"); // Move to the next state
            List<string> messages = session.OnMessage("product");

            Assert.Contains("Sure! I can provide information about our products. What product are you interested in?", messages);
        }

        [Fact]
        public void TestShoppingAssistance()
        {
            Session session = new Session("12345");
            session.OnMessage("hello"); // Move to the next state
            List<string> messages = session.OnMessage("shopping");

            Assert.Contains("Great! How can I assist you with your shopping today?", messages);
        }

        // Add more test methods for other states...

        [Fact]
        public void TestHelpAndSupport()
        {
            Session session = new Session("12345");
            session.OnMessage("hello"); // Move to the next state
            session.OnMessage("help"); // Move to the help and support state
            List<string> messages = session.OnMessage("question");

            Assert.Contains("If you have any questions or need assistance, feel free to ask!", messages);
        }
    }
}
