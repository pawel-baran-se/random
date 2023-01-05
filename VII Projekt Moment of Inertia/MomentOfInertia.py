from math import fabs


class MomentOfInteria(object):
    '''
    Wyznacza moment bezwladnosci przekroju zlozonym z dwoch
    roznych materialow
    '''

    def __init__(self, E1, E2, A1, A2, J1, J2, d1, d2, d3):

        self._E1 = E1
        self._E2 = E2
        self._A1 = A1
        self._A2 = A2
        self._J1 = J1
        self._J2 = J2
        self._d1 = d1
        self._d2 = d2
        self._d3 = d3

    def weight(self):

        n = round(self._E2/self._E1, 2)
        return n 

    def sectionmodulus(self):
        
        n = self.weight()
        se = round(self._A1*(self._d1-self._d3) + n*self._A2*(self._d2-self._d3),1)
        return se

    def totalarea(self):

        n = self.weight()
        tota = self._A1 + n*self._A2
        return tota

    def distance(self):

        se = self.sectionmodulus()
        tota = self.totalarea()
        z = round(se/tota,1)
        return z

    def distancecheck(self, a, b):

        z = self.distance()
        dif = a - b
        if dif > 0:
            fdist = dif + fabs(z)
        else:
            fdist = dif + fabs(z)

        return fdist

    def momentofinteria(self):

        fdist1 = self.distancecheck(self._d1,self._d3)
        fdist2 = self.distancecheck(self._d2,self._d3)
        n = self.weight()    
        J = round(self._J1+self._A1*(fdist1**2) + n*(self._J2+self._A2*(fdist2**2)),1)
        return J


if __name__ == '__main__':
    test = MomentOfInteria(70,210,8.16,8.75,136.63,68,6.08,7.94,7.04)
    value = test.weight()
    value2 = test.sectionmodulus()
    value3 = test.totalarea()
    value4 = test.distance()
    value5 = test.momentofinteria()
    print(value)
    print(value2)
    print(value3)
    print(value4)
    print(value5)