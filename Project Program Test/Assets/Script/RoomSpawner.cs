using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirections;
    // 1 need bottom door
    //2 Need top door
    //3 need left door
    //4 need right door

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;


    void Start(){
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);

    }

    void Spawn(){
        if(spawned == false){
          if(openingDirections == 1){
                    //Spawn room with bottom door
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);

                } else if(openingDirections == 2){
                    //spawn room with top door
                    rand = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                } else if (openingDirections == 3){
                    //spawn room with left door
                        rand = Random.Range(0, templates.leftRooms.Length);
                        Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                } else if (openingDirections == 4){
                    //spawn room with right door
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                }
                spawned = true;


        }

    }
    void onTriggerEnter2D(Collider2D other){
           if(other.CompareTag("SpawnPoint") && other.GetComponent<RoomSpawner>().spawned==true) {
                if(other.GetComponent<RoomSpawner>().spawned == false && spawned==false){
                    Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                }
               Destroy(gameObject);

           }
       }

}
