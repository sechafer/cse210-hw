
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace YouTubeVideoInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select language / Selecciona idioma:");
            Console.WriteLine("1. Español");
            Console.WriteLine("2. English");

            int languageOption;
            if (!int.TryParse(Console.ReadLine(), out languageOption))
            {
                Console.WriteLine("Invalid option. Please select a valid option.");
                return;
            }

            switch (languageOption)
            {
                case 1:
                    MenuPrincipal("es");
                    break;
                case 2:
                    MenuPrincipal("en");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please select a valid option.");
                    break;
            }
        }

        static void MenuPrincipal(string language)
        {
            // Clave de la API de YouTube (debes reemplazarla con tu propia clave)
            string apiKey = "AIzaSyB9NSovIeETU8Iv3YI60OLpKroHahT3yf4";

            // Crea el servicio de YouTube
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = "YouTube Video Info"
            });

            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine(language == "es" ? "Selecciona una opción:" : "Select an option:");
                Console.WriteLine("1. " + (language == "es" ? "Información básica del video" : "Basic video information"));
                Console.WriteLine("2. " + (language == "es" ? "Comentarios del video" : "Video comments"));
                Console.WriteLine("3. " + (language == "es" ? "Salir" : "Exit"));

                int opcion;
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine(language == "es" ? "Opción inválida. Por favor, selecciona una opción válida." : "Invalid option. Please select a valid option.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        ObtenerInformacionBasica(youtubeService, language);
                        break;
                    case 2:
                        ObtenerComentarios(youtubeService, language);
                        break;
                    case 3:
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine(language == "es" ? "Opción inválida. Por favor, selecciona una opción válida." : "Invalid option. Please select a valid option.");
                        break;
                }
            }
        }

        static void ObtenerInformacionBasica(YouTubeService youtubeService, string language)
        {
            string videoId = ObtenerIdVideoDesdeEnlace(language);

            if (string.IsNullOrEmpty(videoId))
            {
                Console.WriteLine(language == "es" ? "No se pudo obtener el ID del video. Verifica el enlace e inténtalo de nuevo." : "Could not get the video ID. Please check the link and try again.");
                return;
            }

            try
            {
                // Obtiene la información básica del video
                var videoRequest = youtubeService.Videos.List("snippet,contentDetails");
                videoRequest.Id = videoId;
                var videoResponse = videoRequest.Execute();

                // Comprueba si se encontró el video
                if (videoResponse.Items.Count > 0)
                {
                    var video = videoResponse.Items[0];

                    // Muestra la información básica del video
                    Console.WriteLine(language == "es" ? "Información del Video:" : "Video Information:");
                    Console.WriteLine(language == "es" ? "Título: " + video.Snippet.Title : "Title: " + video.Snippet.Title);
                    Console.WriteLine(language == "es" ? "Canal: " + video.Snippet.ChannelTitle : "Channel: " + video.Snippet.ChannelTitle);

                    // Obtiene la duración del video en un formato legible
                    var duration = XmlConvert.ToTimeSpan(video.ContentDetails.Duration);
                    Console.WriteLine(language == "es" ? "Duración: " + duration.ToString(@"hh\:mm\:ss") : "Duration: " + duration.ToString(@"hh\:mm\:ss"));
                }
                else
                {
                    Console.WriteLine(language == "es" ? "No se encontró ningún video con el ID proporcionado." : "No video found with the provided ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(language == "es" ? "Error al obtener la información del video: " + ex.Message : "Error getting video information: " + ex.Message);
            }
        }

        static void ObtenerComentarios(YouTubeService youtubeService, string language)
        {
            string videoId = ObtenerIdVideoDesdeEnlace(language);

            if (string.IsNullOrEmpty(videoId))
            {
                Console.WriteLine(language == "es" ? "No se pudo obtener el ID del video. Verifica el enlace e inténtalo de nuevo." : "Could not get the video ID. Please check the link and try again.");
                return;
            }

            try
            {
                // Obtiene los comentarios del video
                var commentThreadsRequest = youtubeService.CommentThreads.List("snippet");
                commentThreadsRequest.VideoId = videoId;
                commentThreadsRequest.MaxResults = 10; // Puedes ajustar este valor según sea necesario
                var commentThreadsResponse = commentThreadsRequest.Execute();

                // Muestra los comentarios en la consola
                Console.WriteLine(language == "es" ? "Comentarios del Video:" : "Video Comments:");
                foreach (var commentThread in commentThreadsResponse.Items)
                {
                    var comment = commentThread.Snippet.TopLevelComment.Snippet;
                    Console.WriteLine(language == "es" ? $"Autor: {comment.AuthorDisplayName}" : $"Author: {comment.AuthorDisplayName}");
                    Console.WriteLine(language == "es" ? $"Comentario: {comment.TextDisplay}" : $"Comment: {comment.TextDisplay}");
                    Console.WriteLine();
                }

                if (commentThreadsResponse.Items.Count == 0)
                {
                    Console.WriteLine(language == "es" ? "El video no tiene comentarios." : "The video has no comments.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(language == "es" ? "Error al obtener los comentarios del video: " + ex.Message : "Error getting video comments: " + ex.Message);
            }
        }

        static string ObtenerIdVideoDesdeEnlace(string language)
        {
            Console.WriteLine(language == "es" ? "Por favor, introduce el enlace completo del video de YouTube:" : "Please enter the full link of the YouTube video:");
            string enlace = Console.ReadLine();

            // Expresión regular para extraer el ID del video del enlace de YouTube
            var regex = new Regex(@"(?:https?:\/\/)?(?:www\.)?youtu(?:\.be|be\.com)\/(?:.*v(?:\/|=)|(?:.*\/)?)([a-zA-Z0-9-_]+)");

            var match = regex.Match(enlace);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return null;
            }
        }
    }
}