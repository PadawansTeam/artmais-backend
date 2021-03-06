namespace ArtmaisBackend.Core.Mail.Factories
{
    public static class BodyFactory
    {
        public static string PaymentBody(string message)
        {
            return $@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
  <head>
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
  </head>

  <body style=""margin: 0; padding: 0"">
    <table
      align=""center""
      border=""0""
      cellpadding=""0""
      cellspacing=""0""
      width=""600""
    >
      <tr>
        <td align=""center"" style=""padding: 40px 0 30px 0"">
          <img
            src=""https://bucket-artmais.s3.amazonaws.com/default-image/log.png""
            width=""300""
            height=""300""
            style=""display: block""
          />
        </td>
      </tr>
      <tr>
        <td bgcolor=""#ffffff"" style=""padding: 30px 30px 30px 30px"">
          <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
            <tr>
              <td style=""font-size: 0; line-height: 0"" width=""20"">&nbsp;</td>
              <td width=""260"" valign=""top"">
                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                  <tr>
                    <td
                      style=""
                        padding: 25px 0 0 0;
                        color: #153643;
                        font-family: Arial, sans-serif;
                        font-size: 24px;
                      ""
                    >
                      <b>{message}</b>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </td>
      </tr>
      <tr>
        <td bgcolor=""#ffffff"" style=""padding: 40px 30px 40px 30px"">
          <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
            <tr>
              <td style=""font-size: 0; line-height: 0"" width=""20"">&nbsp;</td>
              <td width=""260"" valign=""top"">
                <table
                  border=""0""
                  bgcolor=""#9457d5""
                  cellpadding=""0""
                  cellspacing=""0""
                  width=""100%""
                  style=""border-radius: 10px""
                >
                  <tr>
                    <td
                      style=""
                        padding: 25px 25px 25px 25px;
                        color: #ffffff;
                        font-family: Arial, sans-serif;
                        font-size: 24px;
                      ""
                    >
                      &reg; <b>Art+, São Paulo, 2021</b><br />
                      <b>Em caso de dúvidas, contate o suporte através do nosso
                        site.</b>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </body>
</html>
";
        }

        public static string PasswordRecoveryBody(string message)
        {
            return $@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
  <head>
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
  </head>

  <body style=""margin: 0; padding: 0"">
    <table
      align=""center""
      border=""0""
      cellpadding=""0""
      cellspacing=""0""
      width=""600""
    >
      <tr>
        <td align=""center"" style=""padding: 40px 0 30px 0"">
          <img
            src=""https://bucket-artmais.s3.amazonaws.com/default-image/log.png""
            width=""300""
            height=""300""
            style=""display: block""
          />
        </td>
      </tr>
      <tr>
        <td bgcolor=""#ffffff"" style=""padding: 30px 30px 30px 30px"">
          <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
            <tr>
              <td style=""font-size: 0; line-height: 0"" width=""20"">&nbsp;</td>
              <td width=""260"" valign=""top"">
                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                  <tr>
                    <td
                      style=""
                        padding: 25px 0 0 0;
                        color: #153643;
                        font-family: Arial, sans-serif;
                        font-size: 24px;
                        text-align: center;
                      ""
                    >
                      <b>{message}</b>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </td>
      </tr>
      <tr>
        <td bgcolor=""#ffffff"" style=""padding: 40px 30px 40px 30px"">
          <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
            <tr>
              <td style=""font-size: 0; line-height: 0"" width=""20"">&nbsp;</td>
              <td width=""260"" valign=""top"">
                <table
                  border=""0""
                  bgcolor=""#9457d5""
                  cellpadding=""0""
                  cellspacing=""0""
                  width=""100%""
                  style=""border-radius: 10px""
                >
                  <tr>
                    <td
                      style=""
                        padding: 25px 25px 25px 25px;
                        color: #ffffff;
                        font-family: Arial, sans-serif;
                        font-size: 24px;
                      ""
                    >
                      &reg; <b>Art+, São Paulo, 2021</b><br />
                      <b>Em caso de dúvidas, contate o suporte através do nosso
                        site.</b>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </body>
</html>
";
        }
    }
}