﻿using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "storehouse@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\storehouse_emails";
    }
    
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Entities.Cart cart, Entities.ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = @"c:\storehouse_emails";
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                .AppendLine("Новый заказ обработан")
                .AppendLine("---")
                .AppendLine("Товары:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Material.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (итого: {2:c}", line.Quantity, line.Material.Title, subtotal);
                }

                body.AppendFormat("Общая стоимость: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Доставка:")
                    .AppendLine(shippingDetails.Name + "\n")
                    .AppendLine(shippingDetails.Location + "\n")
                    .AppendLine(shippingDetails.City + "\n")
                    .AppendLine(shippingDetails.Country + "\n")
                    .AppendLine("---")
                    .AppendLine("Оплачено со счета: \n" + shippingDetails.paymentBill)
                    .AppendFormat("Подарочная упаковка: {0}", shippingDetails.GiftWrap ? "Да" : "Нет");

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "Новый заказ отправлен!",
                    body.ToString()
                    );

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }

        public void ProcessResponse(Entities.Orders order)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = @"c:\engageOrders_emails";
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                .AppendLine("Ваш заказ был передан службе доставки")
                .AppendLine("---");

               string MailToAddress = order.Email;

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "Ваш заказ передан службе доставки!",
                    body.ToString()
                    );

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
    }

