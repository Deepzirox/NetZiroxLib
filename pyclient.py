import socket
import time

HOST = '127.0.0.1'  # dirección IP del servidor
PORT = 8090        # puerto del servidor


# crea un socket TCP/IP
with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    # conecta el socket al servidor
    s.connect((HOST, PORT))
    
    # envía mensajes durante 10 segundos
    for i in range(20):
        # Obtener la dirección IP y el puerto del socket
        ip, port = s.getsockname()

        # Convertir a string
        ip_str = str(ip)
        port_str = str(port)
        s.sendall(f'Client(ip="{ip_str}, port={port_str}, data={i}")-'.encode())
        time.sleep(1)
    
    # cierra la conexión
    s.close()