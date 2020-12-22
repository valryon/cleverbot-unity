using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace CleverbotUnity
{
    /// <summary>
    /// Cleverbot API wrapper for Unity.
    /// </summary>
    /// <remarks>Based on https://github.com/velddev/Cleverbot.Net </remarks>
    public class Cleverbot
    {
        private string ApiKey { get; }

        /// <summary>
        /// Creates a Cleverbot instance.
        /// </summary>
        /// <param name="apikey">Your api key obtained from https://cleverbot.com/api/ </param>
        public Cleverbot(string apikey)
        {
            if (string.IsNullOrWhiteSpace(apikey))
            {
                throw new Exception("You can't connect without a API key.");
            }

            ApiKey = apikey;
        }

        /// <summary>
        /// Send a message to cleverbot asynchronously and get a response.
        /// </summary>
        /// <param name="message">your message sent to cleverbot</param>
        /// <returns>response from the cleverbot.com api</returns>
        public Task<CleverbotResponse> GetResponseAsync(string message)
        {
            return CleverbotResponse.CreateAsync(message, "", ApiKey);
        }
    }

    public class CleverbotResponse
    {
        #region Fields

        // ReSharper disable InconsistentNaming
        [JsonProperty("interaction_count")] public string interactionCount;

        [JsonProperty("input")] public string inputMessage;

        [JsonProperty("predicted_input")]
        // ("predicted_input")]
        public string predictedInputMessage;

        // ("accuracy")]
        public string accuracy;

        // ("output_label")]
        public string outputLabel;

        [JsonProperty("output")] public string output;

        [JsonProperty("conversation_id")] public string conversationId;

        // ("errorline")]
        public string errorLine;

        // ("database_version")]
        public string databaseVersion;

        // ("software_version")]
        public string softwareVersion;

        // ("time_taken")]
        public string timeTaken;

        // ("random_number")]
        public string randomNumber;

        [JsonProperty("time_second")] public string timeSeconds;

        [JsonProperty("time_minute")] public string timeMinutes;

        [JsonProperty("time_hour")] public string timeHours;

        // ("time_day_of_week")]
        public string timeDayOfWeek;

        [JsonProperty("time_day")] public string timeDays;

        [JsonProperty("time_month")] public string timeMonths;

        [JsonProperty("time_year")] public string timeYears;

        // ("reaction")]
        public string reaction;

        // ("reaction_tone")]
        public string reactionTone;

        // ("emotion")]
        public string emotion;

        // ("emotion_tone")]
        public string emotionTone;

        // ("clever_accuracy")]
        public string cleverAccuracy;

        // ("clever_output")]
        public string cleverOutput;

        // ("clever_match")]
        public string cleverMatch;

        // ("time_elapsed")]
        public string timeElapsed;

        // ("filtered_input")]
        public string filteredInput;

        // ("reaction_degree")]
        public string reactionDegree;

        // ("emotion_degree")]
        public string emotionDegree;

        // ("reaction_values")]
        public string reactionValues;

        // ("emotion_values")]
        public string emotionValues;

        // ("callback")]
        public string callback;

        // ("interaction_1")]
        public string interaction1;

        // ("interaction_2")]
        public string interaction2;

        // ("interaction_3")]
        public string interaction3;

        // ("interaction_4")]
        public string interaction4;

        // ("interaction_5")]
        public string interaction5;

        // ("interaction_6")]
        public string interaction6;

        // ("interaction_7")]
        public string interaction7;

        // ("interaction_8")]
        public string interaction8;

        // ("interaction_9")]
        public string interaction9;

        // ("interaction_10")]
        public string interaction10;

        // ("interaction_11")]
        public string interaction11;

        // ("interaction_12")]
        public string interaction12;

        // ("interaction_13")]
        public string interaction13;

        // ("interaction_14")]
        public string interaction14;

        // ("interaction_15")]
        public string interaction15;

        // ("interaction_16")]
        public string interaction16;

        // ("interaction_17")]
        public string interaction17;

        // ("interaction_18")]
        public string interaction18;

        // ("interaction_19")]
        public string interaction19;

        // ("interaction_20")]
        public string interaction20;

        // ("interaction_21")]
        public string interaction21;

        // ("interaction_22")]
        public string interaction22;

        // ("interaction_23")]
        public string interaction23;

        // ("interaction_24")]
        public string interaction24;

        // ("interaction_25")]
        public string interaction25;

        // ("interaction_26")]
        public string interaction26;

        // ("interaction_27")]
        public string interaction27;

        // ("interaction_28")]
        public string interaction28;

        // ("interaction_29")]
        public string interaction29;

        // ("interaction_30")]
        public string interaction30;

        // ("interaction_31")]
        public string interaction31;

        // ("interaction_32")]
        public string interaction32;

        // ("interaction_33")]
        public string interaction33;

        // ("interaction_34")]
        public string interaction34;

        // ("interaction_35")]
        public string interaction35;

        // ("interaction_36")]
        public string interaction36;

        // ("interaction_37")]
        public string interaction37;

        // ("interaction_38")]
        public string interaction38;

        // ("interaction_39")]
        public string interaction39;

        // ("interaction_40")]
        public string interaction40;

        // ("interaction_41")]
        public string interaction41;

        // ("interaction_42")]
        public string interaction42;

        // ("interaction_43")]
        public string interaction43;

        // ("interaction_44")]
        public string interaction44;

        // ("interaction_45")]
        public string interaction45;

        // ("interaction_46")]
        public string interaction46;

        // ("interaction_47")]
        public string interaction47;

        // ("interaction_48")]
        public string interaction48;

        // ("interaction_49")]
        public string interaction49;

        // ("interaction_50")]
        public string interaction50;

        // ("interaction_1_other")]

        public string interaction1other;

        // ReSharper restore InconsistentNaming

        #endregion

        /// <summary>
        /// Id to keep track of the conversation
        /// </summary>
        public string ConversationId => conversationId;

        /// <summary>
        /// Cleverbot's response message
        /// </summary>
        public string Response => output;

        /// <summary>
        /// The user's latest message
        /// </summary>
        public string Input => inputMessage;

        private string apiKey;

        internal static async Task<CleverbotResponse> CreateAsync(string message, string conversationId, string apiKey)
        {
            HttpClient c = new HttpClient();

            string conversationLine = (string.IsNullOrWhiteSpace(conversationId) ? "" : $"&cs={conversationId}");

            byte[] bytesReceived = await c
                .GetByteArrayAsync(
                    $"https://www.cleverbot.com/getreply?key={apiKey}&wrapper=cleverbot.net&input={message}{conversationLine}")
                .ConfigureAwait(false);

            if (bytesReceived == null) return null;
            string result = Encoding.UTF8.GetString(bytesReceived, 0, bytesReceived.Length);
            CleverbotResponse response = JsonConvert.DeserializeObject<CleverbotResponse>(result);
            if (response == null) return null;
            response.apiKey = apiKey;

            return response;
        }
        
        public CleverbotResponse Respond(string text)
        {
            return CreateAsync(text, ConversationId, apiKey).GetAwaiter().GetResult();
        }

        public Task<CleverbotResponse> RespondAsync(string text)
        {
            return CreateAsync(text, conversationId, apiKey);
        }
    }
}