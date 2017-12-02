# -*- coding: utf-8 -*-
"""
Created on Fri Feb 17 16:44:58 2017

@author: ramai
"""

from socket import *
import struct
import os
import threading
import pickle

##Server Setup##
serverPort = 5000
serverSocket = socket(AF_INET,SOCK_STREAM)
serverSocket.bind(('',serverPort))
threads = []
print ('The server is ready to receive')
################

def recvall(sock, count):
    buf = b''
    while count:
        newbuf = sock.recv(count)
        if not newbuf: return None
        buf += newbuf
        count -= len(newbuf)
    return buf


def recv_one_message(sock):
    lengthbuf = recvall(sock,4)
    length, = struct.unpack('!I',lengthbuf)
    return recvall(sock,length)
    
def getSize(fileName):
    st = os.stat(fileName)
    return st.st_size


def send_one_message(sock,data):
    length = len(data)
    sock.sendall(struct.pack('!I', length))
    sock.sendall(data)

##Command Launcher##

def launchCommand(commandIn):
    if(commandIn.upper() == "GET"):
        
        print("Sending OK")
        
        connectionSocket.send("OK".encode())
        
        print("OK sent")
        sendFile()
        #print("Not implemented.")
    elif(commandIn.upper() == "LIST"):
        #launch list
        sendList()
    else:
        print("Invalid command.")
        return None
    
    
##Server-side get command##

def sendFile():
    #recieve the file name
    fileName = connectionSocket.recv(1024).decode()
    print("Received file name.")
    
    #Check if the file exists
    if(os.path.isfile(fileName)):
        #Send file size
        size = str(getSize(fileName))
        #print(fileName + "is " + size)
        connectionSocket.send(size.encode())
        
        print("Sent file size.")
        
        #Retrieve OK to send.
        isOK = connectionSocket.recv(1024).decode()
        
        print("Sending File...")
        
        #Send file
        theFile = open(fileName, "rb")
        fileData = theFile.read()
        theFile.close()
        send_one_message(connectionSocket,fileData)
        
        print("File Sent")
        
    else:
        print("File not found.")
        #Send NOTOK
        connectionSocket.send("NOTOK".encode())
    
    

###########################

def handler(connectionSocket):
    while True:
        commandIn = connectionSocket.recv(1024).decode()
        if(commandIn.upper() == "EXIT"):
            print("Connection closed.")
            connectionSocket.close()
            return None
        else:
            launchCommand(commandIn)


##Return List##

def sendList():
    theList = os.listdir()
    data=pickle.dumps(theList)
    send_one_message(connectionSocket,data)
    print("List sent.")


##Threading##

class myThread(threading.Thread):
    def __init__(self, ip, port):
        threading.Thread.__init__(self)
        self.ip = ip
        self.port = port
    def run(self):
        #print("Starting " + self.name)
        handler(self.ip)
        #print("Exiting " + self.name)

while True:
    serverSocket.listen(10)
    connectionSocket,addr = serverSocket.accept()
    newThread = myThread(connectionSocket, addr)
    newThread.start()
    threads.append(newThread)

for x in threads:
    x.join()
    