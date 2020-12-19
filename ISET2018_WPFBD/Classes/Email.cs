using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail; //MailMessage, SmtpClient
using System.Net.Mime; //MediaTypesNames
using System.Net;
using System.Net.Configuration;
using System.Collections;
using System.Text.RegularExpressions;

namespace ISET2018_WPFBD.Classes
{
    public class Email
    {
        MailMessage message;
        string login = "maximemaes9@gmail.com";
        string mdp = "pihjzxwjvxtjvevb"; //Mot de passe pour application google

        public Email()
        {

        }

        public void Envoyer(string destinataire)
        {
            SmtpClient client = new SmtpClient();

            //Réglages
            client.Port = 587; //Port utilisé
            client.Host = "smtp.gmail.com"; //Serveur smtp
            client.Timeout = 10000; //Délai d'attente
            client.DeliveryMethod = SmtpDeliveryMethod.Network; //Méthode d'envoi
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;  //Connexion sécurisée
            client.Credentials = new NetworkCredential(login, mdp); //Authentification

            string objetMail = "Alerte arrivée voiture";
            string contenuMessage = "Une voiture répondant à votre desiderata vient d'arriver à notre garage";

            message = new MailMessage(login, destinataire, objetMail, contenuMessage); //Contient les données du mail

            if (message != null && destinataire != "") //Si le message n'est pas vide
            {
                    //client.Send(message); //envoi du message
                    MessageBox.Show("Message envoyé avec succès à :  " + destinataire);
            }
            else
            {
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            }
        }
    }
}
