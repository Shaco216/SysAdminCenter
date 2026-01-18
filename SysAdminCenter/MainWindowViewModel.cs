using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;

namespace SysAdminCenter;

public class MainWindowViewModel
{

    private WindowManager _windowmanager;

    public MainWindowViewModel()
    {
        _windowmanager = new WindowManager();

        OpenConnectionCommand = new RelayCommand(OnOpenConnectionCommand);
        NewConnectionCommand = new RelayCommand(OnNewConnectionCommand);
    }

    private async void OnNewConnectionCommand()
    {
        // Beispiel: Öffne ein Dialog zur Eingabe neuer Verbindungsdaten.
        // Implementiere UI-Logik hier.
        _windowmanager.ShowWindow(new AddConnectionViewModel());
    }

    private async void OnOpenConnectionCommand()
    {
        try
        {
            // Beispielwerte — ersetze durch UI-Eingaben
            string host = "dc01.example.local";
            int port = 636;
            bool useSsl = true;
            string username = "user@example.com"; // oder use null + DefaultNetworkCredentials
            string password = "P@ssw0rd";
            string baseDn = "DC=example,DC=local";
            string filter = "(objectClass=user)";

            var entries = await ConnectAndSearchAsync(host, port, useSsl, username, password, baseDn, filter);

            // TODO: Ergebnis verarbeiten (UI-Update)
            Console.WriteLine($"Gefundene Einträge: {entries.Count}");
        }
        catch (Exception ex)
        {
            // TODO: Fehler anzeigen (z. B. MessageBox oder Logging)
            Console.WriteLine(ex);
        }
    }

    public IRelayCommand OpenConnectionCommand { get; }
    public IRelayCommand NewConnectionCommand { get; }

    private Task<List<Dictionary<string, string>>> ConnectAndSearchAsync(
        string host,
        int port,
        bool useSsl,
        string username,
        string password,
        string baseDn,
        string ldapFilter)
    {
        return Task.Run(() =>
        {
            var results = new List<Dictionary<string, string>>();

            var identifier = new LdapDirectoryIdentifier(host, port);
            using var connection = new LdapConnection(identifier)
            {
                Timeout = TimeSpan.FromSeconds(10)
            };

            // Option A: Integrierte Anmeldeinformationen (Domain-joined)
            // connection.AuthType = AuthType.Negotiate;
            // var credential = CredentialCache.DefaultNetworkCredentials;

            // Option B: Benutzername / Passwort
            var credential = new NetworkCredential(username, password);
            connection.AuthType = AuthType.Negotiate; // oder AuthType.Basic bei Bedarf (nur über SSL)

            if (useSsl)
            {
                connection.SessionOptions.SecureSocketLayer = true;
                // Hinweis: Implementiere hier eine echte Zertifikatsprüfung für Produktion.
                // connection.SessionOptions.VerifyServerCertificate += (conn, cert) => { /* validieren */ return true; };
            }

            // Binden (Authentifizieren)
            connection.Bind(credential);

            // Suche ausführen
            var request = new SearchRequest(baseDn, ldapFilter, SearchScope.Subtree, null);
            var response = (SearchResponse)connection.SendRequest(request);

            foreach (SearchResultEntry entry in response.Entries)
            {
                var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) { ["distinguishedName"] = entry.DistinguishedName };
                foreach (DirectoryAttribute attr in entry.Attributes.Values)
                {
                    if (attr.Count > 0)
                        dict[attr.Name] = attr[0]?.ToString() ?? string.Empty;
                }
                results.Add(dict);
            }

            return results;
        });
    }
}
