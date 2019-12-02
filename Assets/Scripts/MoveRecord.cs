using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRecord
{
    public MoveRecord(MoveRecord record) {
        movesMade = record.movesMade;
    }
    public MoveRecord() {
        movesMade = "";
    }
    public string movesMade;
}
