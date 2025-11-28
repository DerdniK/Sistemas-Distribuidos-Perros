
package SOAPclient;

import jakarta.xml.bind.annotation.XmlRegistry;


/**
 * This object contains factory methods for each 
 * Java content interface and Java element interface 
 * generated in the SOAPclient package. 
 * <p>An ObjectFactory allows you to programatically 
 * construct new instances of the Java representation 
 * for XML content. The Java representation of XML 
 * content can consist of schema derived interfaces 
 * and classes representing the binding of schema 
 * type definitions, element declarations and model 
 * groups.  Factory methods for each of these are 
 * provided in this class.
 * 
 */
@XmlRegistry
public class ObjectFactory {


    /**
     * Create a new ObjectFactory that can be used to create new instances of schema derived classes for package: SOAPclient
     * 
     */
    public ObjectFactory() {
    }

    /**
     * Create an instance of {@link CreateChampionRequest }
     * 
     * @return
     *     the new instance of {@link CreateChampionRequest }
     */
    public CreateChampionRequest createCreateChampionRequest() {
        return new CreateChampionRequest();
    }

    /**
     * Create an instance of {@link Champion }
     * 
     * @return
     *     the new instance of {@link Champion }
     */
    public Champion createChampion() {
        return new Champion();
    }

    /**
     * Create an instance of {@link CreateChampionResponse }
     * 
     * @return
     *     the new instance of {@link CreateChampionResponse }
     */
    public CreateChampionResponse createCreateChampionResponse() {
        return new CreateChampionResponse();
    }

    /**
     * Create an instance of {@link GetChampionRequest }
     * 
     * @return
     *     the new instance of {@link GetChampionRequest }
     */
    public GetChampionRequest createGetChampionRequest() {
        return new GetChampionRequest();
    }

    /**
     * Create an instance of {@link GetChampionResponse }
     * 
     * @return
     *     the new instance of {@link GetChampionResponse }
     */
    public GetChampionResponse createGetChampionResponse() {
        return new GetChampionResponse();
    }

}
