package ChampionSelect.ChampionSelect.Client;

import ChampionSelect.ChampionSelect.Exception.SoapClientException;
import ChampionSelect.ChampionSelect.Models.Champion;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.ws.client.core.WebServiceTemplate;
import org.springframework.stereotype.Component;

@Component
public class ChampionSoapClient {
    
    private final WebServiceTemplate webServiceTemplate;

    @Value("${champions.soap.service-url}") 
    private String soapServiceUrl;

    public ChampionSoapClient(WebServiceTemplate webServiceTemplate) {
        this.webServiceTemplate = webServiceTemplate;
    }

    public Champion createChampion(Champion champion) {
        try {
            return champion;
        } catch (Exception e) {
            throw new SoapClientException("Error al crear campeón en SOAP: " + e.getMessage(), e);
        }
    }
    
    public Champion updateChampion(Long id, Champion champion) {
        try {
            return champion;
        } catch (Exception e) {
            throw new SoapClientException("Error al actualizar campeón en SOAP: " + e.getMessage(), e);
        }
    }

    public void deleteChampion(Long id) {
        try {
        } catch (Exception e) {
            throw new SoapClientException("Error al eliminar campeón en SOAP: " + e.getMessage(), e);
        }
    }
}