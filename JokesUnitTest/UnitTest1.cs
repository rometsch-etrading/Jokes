using System;
using System.Linq;
using Jokes.Models;
using Jokes.Persistence;
using Jokes.Webservices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace JokesUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private SQLiteJokeStore store;

        /// <summary>
        /// Initializes Database Store
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            store = new SQLiteJokeStore(new SQLiteDb());
        }

        /// <summary>
        /// Test an API Call
        /// Check for success status code
        /// </summary>
        [TestMethod]
        public void APICallTest()
        {

            WSClient client = new WSClient();
            var jotd = client.Get<JokeOfTheDayWSResult>("https://icanhazdadjoke.com/").Result;
            Assert.AreEqual(jotd.status, 200);
        }

        /// <summary>
        /// Tests Fetch of full List
        /// </summary>
        [TestMethod]
        public void GetJokesTest()
        {
            var joke1 = new Joke
            {
                Text = "New Joke 1",
                Date = DateTime.Now
            };

            var joke2 = new Joke
            {
                Text = "New Joke 2",
                Date = DateTime.Now
            };

            var joke3 = new Joke
            {
                Text = "New Joke 3",
                Date = DateTime.Now
            };

            // Create 3 Jokes
            store.AddJokeAsync(joke1).Wait();
            store.AddJokeAsync(joke2).Wait();
            store.AddJokeAsync(joke3).Wait();

            // Test
            var jokes = store.GetJokesAsync();
            Assert.AreEqual(3, jokes.Result.Count());

            // Delete the 3 Jokes again
            store.DeleteJokeAsync(joke1).Wait();
            store.DeleteJokeAsync(joke2).Wait();
            store.DeleteJokeAsync(joke3).Wait();
        }

        /// <summary>
        /// Tests CRUD Operations
        /// </summary>
        [TestMethod]
        public void CRUDTest()
        {
            var joke = new Joke
            {
                Text = "New Joke",
                Date = DateTime.Now
            };

            // INSERT
            var success = store.AddJokeAsync(joke).Result;
            Assert.AreEqual(1, success);

            // GET
            Joke insertedJoke = store.GetJokeAsync(joke.Id).Result;
            Assert.AreEqual("New Joke", insertedJoke.Text);

            // UPDATE
            joke.Text = "Updated Joke";
            store.UpdateJokeAsync(joke).Wait();
            Assert.AreEqual("Updated Joke", store.GetJokeAsync(joke.Id).Result.Text);

            // DELETE
            store.DeleteJokeAsync(joke).Wait();
            Assert.IsNull(store.GetJokeAsync(joke.Id).Result);
        }
    }
}
