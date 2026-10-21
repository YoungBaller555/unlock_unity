using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;


public class NewBehaviourScript : MonoBehaviour
{
    public OSCReceiver oscReceiver;
    public OSCTransmitter oscTransmitter;

    public Unlock unlock;
    // Start is called before the first frame update
    void Start()
    {
        oscReceiver.Bind("/alpha", TraiterMessageAlpha);
        oscReceiver.Bind("/beta", TraiterMessageBeta);

    }

    // Update is called once per frame
    void Update()
    {
        if ( unlock.isUnlocked() ) {
        var oscMessage = new OSCMessage("/lumiere"); 
        oscMessage.AddValue( OSCValue.Int(1) ); // Ajoute l’entier 1
        oscTransmitter.Send( oscMessage ); // Envoyer le message 
        } else {
        var oscMessage = new OSCMessage("/lumiere"); 
        oscMessage.AddValue( OSCValue.Int(0) ); // Ajoute l’entier 0
        oscTransmitter.Send( oscMessage ); // Envoyer le message 
        }

    }

    void TraiterMessageAlpha(OSCMessage message)
{
    
    // Traiter un entier booléen (évènement discret)

    // Récupérer la valeur du premier argument en taant qu’entier :
    int valeur = message.Values[0].IntValue;

    // Deboguer
    // Debug.Log("Reçu : " + message.Address + " " + valeur);
    float ajuste = (((float)valeur - potInMin) / (potInMax - potInMin) * (potOutMax - potOutMin) + potOutMin);
    unlock.SetAngle(ajuste);
    
    
}

void TraiterMessageBeta(OSCMessage message)
{
    
    // Traiter un entier booléen (évènement discret)

    // Récupérer la valeur du premier argument en taant qu’entier :
    int valeur = message.Values[0].IntValue;

    // Deboguer
    // Debug.Log("Reçu : " + message.Address + " " + valeur);

    // Traiter la valeur
    if ( valeur == 1 ) {
        unlock.CheckSolution();
    } else {
        // Si nécéssaire, appeler une fonction ici
    }

}


}
