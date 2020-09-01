using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Mail;
using UnityEngine.UI;


public class Mailer : MonoBehaviour
{
  public InputField iField;
  private string recipientEmail;
  public MeshCombine combineMeshes;

    public void Send(){

combineMeshes.CombineMeshesAndSave();
recipientEmail = iField.text;
MailMessage mail = new MailMessage();
SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
mail.From = new MailAddress("sicblivn@gmail.com");
mail.To.Add(recipientEmail);
mail.Subject = "AR Clay - Your Newly Sculpted Model";
mail.Body = "Hello! \n Please Find Attached the model you just made with AR CLAY \n Thank you for using our service and please let us know how we can make it better for your \n Enjoy Your Day!";
 
System.Net.Mail.Attachment attachment;
string path = Application.persistentDataPath + "/Export.fbx";
attachment = new System.Net.Mail.Attachment(path);
mail.Attachments.Add(attachment);
 
SmtpServer.Port = 587;
SmtpServer.Credentials = new System.Net.NetworkCredential("sicblivn@gmail.com", "Kaptwamwa4");
SmtpServer.EnableSsl = true;
 
SmtpServer.Send(mail);

  Debug.Log(Application.persistentDataPath);
  Debug.Log("Sent attachment");
  iField.text = "Scuplture Sent!";
    }
    // Update is called once per frame

}
