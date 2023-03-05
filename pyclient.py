import socket
import time

HOST = '127.0.0.1'  # dirección IP del servidor
PORT = 8090        # puerto del servidor
MESSAGE = b'Hello desde python!-'

# crea un socket TCP/IP
with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    # conecta el socket al servidor
    s.connect((HOST, PORT))
    
    # envía mensajes durante 10 segundos
    for i in range(10):
        s.sendall(MESSAGE)
        time.sleep(1)
    
    # cierra la conexión
    s.close()