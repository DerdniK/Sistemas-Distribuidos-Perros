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

    /**
     * Crea un campeón a través del servicio SOAP
     */
    public Champion createChampion(Champion champion) {
        try {
            // Por ahora, solo retornamos el campeón sin llamar al servicio SOAP
            return champion;
        } catch (Exception e) {
            throw new SoapClientException("Error al crear campeón en SOAP: " + e.getMessage(), e);
        }
    }
    
    /**
     * Actualiza un campeón a través del servicio SOAP
     */
    public Champion updateChampion(Long id, Champion champion) {
        try {
            // Por ahora, solo retornamos el campeón sin llamar al servicio SOAP
            return champion;
        } catch (Exception e) {
            throw new SoapClientException("Error al actualizar campeón en SOAP: " + e.getMessage(), e);
        }
    }

    /**
     * Elimina un campeón a través del servicio SOAP
     */
    public void deleteChampion(Long id) {
        try {
            // Por ahora, no hacemos nada
        } catch (Exception e) {
            throw new SoapClientException("Error al eliminar campeón en SOAP: " + e.getMessage(), e);
        }
    }
}