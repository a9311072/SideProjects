# def solution(N):
#     return len(max(format(N,'b').strip('0').split('1')))

# print(solution(1041))

# a = '10000001'
# a = a.strip('0').split('1')
# print(a)

def solution(N):
    bits = format(N, 'b')
    size = 0
    sizeReal = 0
    cont = 0
    for i in range(len(bits)):
        if (bits[i] == '0'):
            if (cont > 0): cont+=1
            else: cont = 1
        else: cont = 0
        if (cont > size): size = cont
        if (bits[i] == '1' and size > sizeReal): sizeReal = size
    return sizeReal


a = 41
print(solution(a))
a = 1041
print(solution(a))
a = 32
print(solution(a))
a = 15
print(solution(a))
a = 9
print(solution(a))
a = 20
print(solution(a))