using System;

public class TCP
{
  public static string TraverseStates(string[] events)
  {
    var state = "CLOSED";
    
    foreach (string e in events)
    {
      state = ProcessState(e, state);  
    }
    
    return state;
  }
  
  static string ProcessState(string input, string currentState)
  {
    switch (currentState)
    {
        case "CLOSED":
          if (input == "APP_PASSIVE_OPEN") return "LISTEN";
          if (input == "APP_ACTIVE_OPEN") return "SYN_SENT";
          return "ERROR";
        case "LISTEN":
          if (input == "RCV_SYN") return "SYN_RCVD";
          if (input == "APP_SEND") return "SYN_SENT";
          if (input == "APP_CLOSE") return "CLOSED";
          return "ERROR";
        case "SYN_RCVD":
          if (input == "APP_CLOSE") return "FIN_WAIT_1";
          if (input == "RCV_ACK") return "ESTABLISHED";
          return "ERROR";
        case "SYN_SENT":
          if (input == "RCV_SYN") return "SYN_RCVD";
          if (input == "RCV_SYN_ACK") return "ESTABLISHED";
          if (input == "APP_CLOSE") return "CLOSED";
          return "ERROR";
        case "ESTABLISHED":
          if (input == "APP_CLOSE") return "FIN_WAIT_1";
          if (input == "RCV_FIN") return "CLOSE_WAIT";
          return "ERROR";
        case "FIN_WAIT_1":
          if (input == "RCV_FIN") return "CLOSING";
          if (input == "RCV_FIN_ACK") return "TIME_WAIT";
          if (input == "RCV_ACK") return "FIN_WAIT_2";
          return "ERROR";
        case "CLOSING":
          if (input == "RCV_ACK") return "TIME_WAIT";
          return "ERROR";
        case "FIN_WAIT_2":
          if (input == "RCV_FIN") return "TIME_WAIT";
          return "ERROR";
        case "TIME_WAIT":
          if (input == "APP_TIMEOUT") return "CLOSED";
          return "ERROR";
        case "CLOSE_WAIT":
          if (input == "APP_CLOSE") return "LAST_ACK";
          return "ERROR";
        case "LAST_ACK":
          if (input == "RCV_ACK") return "CLOSED";
          return "ERROR";
        default: 
          return "ERROR";
    }
  }
}