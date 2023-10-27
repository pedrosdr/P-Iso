using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public static class Beams
    {
        //Gets the position of the nodes of a beam based on all of its loads and supports
        public static double[] GetNodes(Beam beam)
        {
            List<Support> supports = beam.Supports;
            List<Load> loads = beam.PointLoads;
            List<Load> virtualLoads = beam.VirtualPointLoads;
            List<DistributedLoad> distLoads = beam.DistributedLoads;
            List<DistributedLoad> virtualDistLoads = beam.VirtualDistributedLoads;

            SortedSet<double> nodes = new SortedSet<double>();
            nodes.Add(0);
            nodes.Add(beam.Length);
            supports.ForEach(s => nodes.Add(s.InnerPoint));
            loads.ForEach(l => nodes.Add(l.InnerPoint));
            virtualLoads.ForEach(l => nodes.Add(l.InnerPoint));
            distLoads.ForEach(l => nodes.Add(l.InnerP1));
            distLoads.ForEach(l => nodes.Add(l.InnerP2));
            virtualDistLoads.ForEach(l => nodes.Add(l.InnerP1));
            virtualDistLoads.ForEach(l => nodes.Add(l.InnerP2));

            return nodes.ToArray();
        }
        public static double[] GetNodes(Beam beam, Beam other)
        {
            SortedSet<double> set = new SortedSet<double>();

            double[] nodesBeam = GetNodes(beam);
            double[] nodesOther = GetNodes(other);

            nodesBeam.ToList().ForEach(n => set.Add(n));
            nodesOther.ToList().ForEach(n => set.Add(n));

            double[] nodes = set.ToArray();
            return nodes;
        }

        //Gets moment equations of the beam based on its nodes
        public static List<MomentumEquation> GetMomentumEquations(Beam beam)
        {
            if (!beam.IsStable) return null;

            List<MomentumEquation> eqs = new List<MomentumEquation>();
            double[] nodes = GetNodes(beam);

            for (int i = 1; i < nodes.Length; i++)
            {
                double SumF =
                    beam.PointLoads.Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY).Sum() +
                    beam.VirtualPointLoads.Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY).Sum() +
                    beam.DistributedLoads.Where(l => l.Far < nodes[i]).Select(l => l.ResultingForce).Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY).Sum() +
                    beam.VirtualDistributedLoads.Where(l => l.Far < nodes[i]).Select(l => l.ResultingForce).Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY).Sum() +
                    beam.Supports.Select(s => s.Reaction).Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY).Sum();

                double SumFi =
                    beam.PointLoads.Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY * l.InnerPoint).Sum() +
                    beam.VirtualPointLoads.Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY * l.InnerPoint).Sum() +
                    beam.DistributedLoads.Where(l => l.Far < nodes[i]).Select(l => l.ResultingForce).Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY * l.InnerPoint).Sum() +
                    beam.VirtualDistributedLoads.Where(l => l.Far < nodes[i]).Select(l => l.ResultingForce).Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY * l.InnerPoint).Sum() +
                    beam.Supports.Select(s => s.Reaction).Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY * l.InnerPoint).Sum();

                double SumQ =
                    beam.DistributedLoads.Where(dl => dl.Near < nodes[i] && dl.Far >= nodes[i]).Select(dl => dl.Value).Sum() +
                    beam.VirtualDistributedLoads.Where(dl => dl.Near < nodes[i] && dl.Far >= nodes[i]).Select(dl => dl.Value).Sum();
           
                double SumQj =
                    beam.DistributedLoads.Where(dl => dl.Near < nodes[i] && dl.Far >= nodes[i]).Select(dl => dl.Value * dl.Near).Sum() +
                    beam.VirtualDistributedLoads.Where(dl => dl.Near < nodes[i] && dl.Far >= nodes[i]).Select(dl => dl.Value * dl.Near).Sum();

                double SumQjj =
                    beam.DistributedLoads.Where(dl => dl.Near < nodes[i] && dl.Far >= nodes[i]).Select(dl => dl.Value * dl.Near * dl.Near).Sum() +
                    beam.VirtualDistributedLoads.Where(dl => dl.Near < nodes[i] && dl.Far >= nodes[i]).Select(dl => dl.Value * dl.Near * dl.Near).Sum();

                double SumM =
                    beam.PointLoads.Where(l => l.InnerPoint < nodes[i]).Select(l => l.Momentum).Sum() +
                    beam.VirtualPointLoads.Where(l => l.InnerPoint < nodes[i]).Select(l => l.Momentum).Sum() +
                    beam.DistributedLoads.Where(l => l.Far < nodes[i]).Select(l => l.ResultingForce).Where(l => l.InnerPoint < nodes[i]).Select(l => l.Momentum).Sum() +
                    beam.VirtualDistributedLoads.Where(l => l.Far < nodes[i]).Select(l => l.ResultingForce).Where(l => l.InnerPoint < nodes[i]).Select(l => l.Momentum).Sum() +
                    beam.Supports.Select(s => s.Reaction).Where(l => l.InnerPoint < nodes[i]).Select(l => l.Momentum).Sum();

                double a = -0.5 * -SumQ;
                double b = -SumQj + SumF;
                double c = -SumFi - 0.5 * -SumQjj - SumM;

                double from = nodes[i - 1];
                double to = nodes[i];

                eqs.Add(new MomentumEquation(a, b, c, from, to));
            }

            return eqs;
        }

        //Gets moment equations of the first beam based on the nodes of both given beams
        public static List<MomentumEquation> GetMomentumEquations(Beam beam, Beam other)
        {
            List<MomentumEquation> eqs = new List<MomentumEquation>();

            SortedSet<double> set = new SortedSet<double>();

            double[] nodesBeam = GetNodes(beam);
            double[] nodesOther = GetNodes(other);

            nodesBeam.ToList().ForEach(n => set.Add(n));
            nodesOther.ToList().ForEach(n => set.Add(n));

            double[] nodes = set.ToArray();

            List<Load> forces = new List<Load>();
            beam.PointLoads.ForEach(f => forces.Add(f));
            beam.VirtualPointLoads.ForEach(f => forces.Add(f));
            beam.VirtualDistributedLoads.ForEach(l => forces.Add(l.ResultingForce));
            beam.DistributedLoads.ForEach(l => forces.Add(l.ResultingForce));
            beam.Supports.ForEach(s => forces.Add(s.Reaction));

            for (int i = 1; i < nodes.Length; i++)
            {
                double SumF =
                    beam.PointLoads.Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY).Sum() +
                    beam.VirtualPointLoads.Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY).Sum() +
                    beam.DistributedLoads.Where(l => l.Far < nodes[i]).Select(l => l.ResultingForce).Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY).Sum() +
                    beam.VirtualDistributedLoads.Where(l => l.Far < nodes[i]).Select(l => l.ResultingForce).Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY).Sum() +
                    beam.Supports.Select(s => s.Reaction).Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY).Sum();

                double SumFi =
                    beam.PointLoads.Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY * l.InnerPoint).Sum() +
                    beam.VirtualPointLoads.Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY * l.InnerPoint).Sum() +
                    beam.DistributedLoads.Where(l => l.Far < nodes[i]).Select(l => l.ResultingForce).Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY * l.InnerPoint).Sum() +
                    beam.VirtualDistributedLoads.Where(l => l.Far < nodes[i]).Select(l => l.ResultingForce).Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY * l.InnerPoint).Sum() +
                    beam.Supports.Select(s => s.Reaction).Where(l => l.InnerPoint < nodes[i]).Select(l => l.VectorY * l.InnerPoint).Sum();

                double SumQ =
                    beam.DistributedLoads.Where(dl => dl.Near < nodes[i] && dl.Far >= nodes[i]).Select(dl => dl.Value).Sum() +
                    beam.VirtualDistributedLoads.Where(dl => dl.Near < nodes[i] && dl.Far >= nodes[i]).Select(dl => dl.Value).Sum();

                double SumQj =
                    beam.DistributedLoads.Where(dl => dl.Near < nodes[i] && dl.Far >= nodes[i]).Select(dl => dl.Value * dl.Near).Sum() +
                    beam.VirtualDistributedLoads.Where(dl => dl.Near < nodes[i] && dl.Far >= nodes[i]).Select(dl => dl.Value * dl.Near).Sum();

                double SumQjj =
                    beam.DistributedLoads.Where(dl => dl.Near < nodes[i] && dl.Far >= nodes[i]).Select(dl => dl.Value * dl.Near * dl.Near).Sum() +
                    beam.VirtualDistributedLoads.Where(dl => dl.Near < nodes[i] && dl.Far >= nodes[i]).Select(dl => dl.Value * dl.Near * dl.Near).Sum();

                double SumM =
                    beam.PointLoads.Where(l => l.InnerPoint < nodes[i]).Select(l => l.Momentum).Sum() +
                    beam.VirtualPointLoads.Where(l => l.InnerPoint < nodes[i]).Select(l => l.Momentum).Sum() +
                    beam.DistributedLoads.Where(l => l.Far < nodes[i]).Select(l => l.ResultingForce).Where(l => l.InnerPoint < nodes[i]).Select(l => l.Momentum).Sum() +
                    beam.VirtualDistributedLoads.Where(l => l.Far < nodes[i]).Select(l => l.ResultingForce).Where(l => l.InnerPoint < nodes[i]).Select(l => l.Momentum).Sum() +
                    beam.Supports.Select(s => s.Reaction).Where(l => l.InnerPoint < nodes[i]).Select(l => l.Momentum).Sum();

                double a = -0.5 * -SumQ;
                double b = -SumQj + SumF;
                double c = -SumFi - 0.5 * -SumQjj - SumM;

                double from = nodes[i - 1];
                double to = nodes[i];

                eqs.Add(new MomentumEquation(a, b, c, from, to));
            }

            return eqs;
        }
        public static List<ShearEquation> GetShearEquations(Beam beam)
        {
            List<MomentumEquation> momentumEquations = GetMomentumEquations(beam);

            List<ShearEquation> shearEquations = new List<ShearEquation>();
            if (momentumEquations == null) return shearEquations;

            foreach (MomentumEquation meq in momentumEquations)
            {
                double[,] raw = new double[,]
                {
                    {meq.a, 2},
                    {meq.b, 1},
                    {meq.c, 0}
                };

                double[,] treated = Calculus.SimpleDerivativeEquation(raw);

                ShearEquation seq = new ShearEquation(treated[0, 0], treated[1, 0], meq.From, meq.To);
                shearEquations.Add(seq);
            }

            return shearEquations;
        }

        //Gets normal equations of the first beam based on the nodes of both given beams
        public static double[] GetNormals(Beam beam, Beam other)
        {
            beam.ComputeIsostaticHorizontalReactions();

            List<double> normals = new List<double>();

            double[] nodes = GetNodes(beam, other);

            List<Load> forces = new List<Load>();
            beam.PointLoads.ForEach(f => forces.Add(f));
            beam.VirtualPointLoads.ForEach(f => forces.Add(f));
            beam.VirtualDistributedLoads.ForEach(l => forces.Add(l.ResultingForce));
            beam.DistributedLoads.ForEach(l => forces.Add(l.ResultingForce));
            beam.Supports.ForEach(s => forces.Add(s.Reaction));

            for(int i = 0; i < nodes.Length; i++)
            {
                double normal = -forces.Where(f => f.InnerPoint < nodes[i]).Select(f => f.VectorX).Sum();
                normals.Add(normal);
            }

            return normals.ToArray();
        }

        // Gets normal equations
        public static double[] GetNormals(Beam beam)
        {
            List<double> normals = new List<double>();
            double[] nodes = GetNodes(beam);

            List<Load> forces = new List<Load>();
            beam.PointLoads.ForEach(f => forces.Add(f));
            beam.VirtualPointLoads.ForEach(f => forces.Add(f));
            beam.VirtualDistributedLoads.ForEach(l => forces.Add(l.ResultingForce));
            beam.DistributedLoads.ForEach(l => forces.Add(l.ResultingForce));
            beam.Supports.ForEach(s => forces.Add(s.Reaction));

            for (int i = 0; i < nodes.Length; i++)
            {
                double normal = -forces.Where(f => f.InnerPoint < nodes[i]).Select(f => f.VectorX).Sum();
                normals.Add(normal);
            }

            return normals.ToArray();
        }

        //Gets the deformation in function of AE in the given point
        public static double GetDeformationFuncAE(Beam beam, double x)
        {
            beam.ComputeIsostaticHorizontalReactions();
            Beam child = new Beam(beam.P1, beam.P2);

            foreach (Support support in beam.Supports)
            {
                IsoPosition position = support.Position;

                if (support is ApoioMovel)
                    child.AddSupport(new ApoioMovel(position, child));
                else if (support is ApoioFixo)
                    child.AddSupport(new ApoioFixo(position, child));
                else
                    child.AddSupport(new Engaste(position, child));
            }

            child.AddVirtualPointLoad(new Load(x, child, 1, 0, 0));

            child.ComputeIsostaticHorizontalReactions();

            double[] parentNs = GetNormals(beam, child);
            double[] childNs = GetNormals(child, beam);
            double[] nodes = GetNodes(beam, child);

            double def = parentNs[0] * childNs[0] * nodes[0];
            for(int i = 1; i < parentNs.Length; i++)
            {
                def += parentNs[i] * childNs[i] * (nodes[i] - nodes[i - 1]);
            }

            return def;
        }

        //Gets the deformation in function of EI in the given point
        public static double GetDeformationFuncEI(Beam beam, double x, Direction direction)
        {
            return DeformationByPTV(beam, x, direction);
        }

        //Gets the deformation in the point in function of EI using the method PTV
        private static double DeformationByPTV(Beam beam, double x, Direction direction)
        {
            beam.ComputeReactions();
            Beam child = new Beam(beam.P1, beam.P2);

            foreach (Support support in beam.Supports)
            {
                IsoPosition position = support.Position;

                if (support is ApoioMovel)
                    child.AddSupport(new ApoioMovel(position, child));
                else if (support is ApoioFixo)
                    child.AddSupport(new ApoioFixo(position, child));
                else
                    child.AddSupport(new Engaste(position, child));
            }

            if (direction == Direction.Vertical)
                child.AddVirtualPointLoad(new Load(x, child, 0, 1, 0));
            else
                child.AddVirtualPointLoad(new Load(x, child, 1, 0, 0));

            child.ComputeReactions();

            List<MomentumEquation> beamEqs = GetMomentumEquations(beam, child);
            List<MomentumEquation> childEqs = GetMomentumEquations(child, beam);

            double sum = 0;
            for (int i = 0; i < beamEqs.Count; i++)
            {
                MomentumEquation M = beamEqs[i];
                MomentumEquation m = childEqs[i];

                double t1 = M.a * m.a;
                double t2 = M.a * m.b + M.b * m.a;
                double t3 = M.a * m.c + M.b * m.b + M.c * m.a;
                double t4 = M.b * m.c + M.c * m.b;
                double t5 = M.c * m.c;

                double[,] multiplierExpoentPairs = new double[,]
                {
                    {t1, 4},
                    {t2, 3},
                    {t3, 2},
                    {t4, 1},
                    {t5, 0}
                };

                double from = beamEqs[i].From;
                double to = beamEqs[i].To;

                try
                {
                    sum += Calculus.SimpleIntegral(multiplierExpoentPairs, to, from);
                }
                catch
                {
                    sum += 0;
                }
            }

            return sum;
        }

        private static double DeformationByEquations(Beam beam, double x)
        {
            DeformationEquation eq = GetDeformationEquations(beam)
                .Where(e => e.From <= x && e.To >= x).FirstOrDefault();

            if (eq == null)
                throw new IsoException("DeformationEquation was null");

            return eq.GetDeformation(x);
        }

        public static List<DeformationEquation> GetDeformationEquations(Beam beam)
        {
            List<DeformationEquation> deformationEquations = new List<DeformationEquation>();

            List<RotationEquation> rotationEquations = GetRotationEquations(beam);

            if (deformationEquations == null || rotationEquations == null)
                return null;

            for (int i = 0; i < rotationEquations.Count; i++)
            {
                RotationEquation rEq = rotationEquations[i];

                double[,] raw = new double[,]
                {
                    {rEq.a, 3},
                    {rEq.b, 2},
                    {rEq.c, 1},
                    {rEq.d, 0}
                };

                double[,] treated = Calculus.SimpleIntegralEquation(raw);
                double defInFrom = DeformationByPTV(beam, rEq.From, Direction.Vertical);
                DeformationEquation dEq = new DeformationEquation(treated[0, 0], treated[1, 0], treated[2, 0], treated[3, 0], defInFrom, rEq.From, rEq.To);
                double defInZero = defInFrom - dEq.a * Math.Pow(dEq.From, 4) - dEq.b * Math.Pow(dEq.From, 3) - dEq.c * Math.Pow(dEq.From, 2) - dEq.d * dEq.From;
                dEq.e = defInZero;
                deformationEquations.Add(dEq);
            }

            return deformationEquations;
        }

        //Gets the rotation in function of EI in the given point
        public static double GetRotationFuncEI(Beam beam, double x)
        {
            return RotationByPTV(beam, x);
        }

        //Gets the rotation in the point in function of EI using the method PTV
        private static double RotationByPTV(Beam beam, double x)
        {
            beam.ComputeReactions();
            Beam child = new Beam(beam.P1, beam.P2);

            foreach (Support support in beam.Supports)
            {
                IsoPosition position = support.Position;

                if (support is ApoioMovel)
                    child.AddSupport(new ApoioMovel(position, child));
                else if (support is ApoioFixo)
                    child.AddSupport(new ApoioFixo(position, child));
                else
                    child.AddSupport(new Engaste(position, child));
            }

            child.AddVirtualPointLoad(new Load(x, child, 0, 0, 1));

            child.ComputeReactions();

            List<MomentumEquation> beamEqs = GetMomentumEquations(beam, child);
            List<MomentumEquation> childEqs = GetMomentumEquations(child, beam);

            double sum = 0;
            for (int i = 0; i < beamEqs.Count; i++)
            {
                MomentumEquation M = beamEqs[i];
                MomentumEquation m = childEqs[i];

                double t1 = M.a * m.a;
                double t2 = M.a * m.b + M.b * m.a;
                double t3 = M.a * m.c + M.b * m.b + M.c * m.a;
                double t4 = M.b * m.c + M.c * m.b;
                double t5 = M.c * m.c;

                double[,] multiplierExpoentPairs = new double[,]
                {
                    {t1, 4},
                    {t2, 3},
                    {t3, 2},
                    {t4, 1},
                    {t5, 0}
                };

                double from = beamEqs[i].From;
                double to = beamEqs[i].To;

                try
                {
                    sum += Calculus.SimpleIntegral(multiplierExpoentPairs, to, from);
                }
                catch
                {
                    sum += 0;
                }
            }

            return sum;
        }

        /*Gets the rotation in the point in function of EI by integrating the momentum equation.
         *It uses the PVT method to get the rotation in the point 0 though */
        private static double RotationByEquations(Beam beam, double x)
        {
            RotationEquation eq = GetRotationEquations(beam)
                .Where(e => e.From <= x && e.To >= x).FirstOrDefault();

            if (eq == null)
                throw new IsoException("RotationEquation was null");

            return eq.GetRotation(x);
        }

        /*Gets the rotation equations in function of EI by integrating the momentum equation.
         *It uses the PVT method to get the rotation in the point 0 though */
        public static List<RotationEquation> GetRotationEquations(Beam beam)
        {
            List<RotationEquation> rotationEquations = new List<RotationEquation>();

            List<MomentumEquation> momentumEquations = GetMomentumEquations(beam);

            if(rotationEquations == null || momentumEquations == null)
                return null;

            for (int i = 0; i < momentumEquations.Count; i++)
            {
                MomentumEquation mEq = momentumEquations[i];

                double[,] raw = new double[,]
                {
                    {mEq.a, 2},
                    {mEq.b, 1 },
                    {mEq.c, 0}
                };

                double[,] treated = Calculus.SimpleIntegralEquation(raw);
                double rotationInFrom = RotationByPTV(beam, mEq.From);
                RotationEquation rEq = new RotationEquation(treated[0, 0], treated[1, 0], treated[2, 0], 0, mEq.From, mEq.To);
                double rotationInZero = rotationInFrom - rEq.a * Math.Pow(rEq.From, 3) - rEq.b * Math.Pow(rEq.From, 2) - rEq.c * rEq.From;
                rEq.d = rotationInZero;
                rotationEquations.Add(rEq);
            }

            return rotationEquations;
        }

        //Gets the moments at the lower and upper limits of the momentum equation and the maximums and minimuns of the function
        public static List<Momentum> GetImportantMomentums(Beam beam)
        {
            List<MomentumEquation> eqs = GetMomentumEquations(beam);

            List<Momentum> momentums = new List<Momentum>();

            foreach(MomentumEquation eq in eqs)
            {
                //on the nodes
                double x;
                double M;
                Momentum momentum;

                x = Math.Round(eq.From, 2);
                M = Math.Round(eq.a * x * x + eq.b * x + eq.c, 2);

                momentum = new Momentum(M, x);
                if (!momentums.Contains(momentum))
                    momentums.Add(momentum);

                x = Math.Round(eq.To, 2);
                M = Math.Round(eq.a * x * x + eq.b * x + eq.c, 2);

                momentum = new Momentum(M, x);
                if (!momentums.Contains(momentum))
                    momentums.Add(momentum);

                //max and min on the parabola
                if(eq.a != 0)
                {
                    double delta = eq.b * eq.b - 4 * eq.a * eq.c;

                    x = Math.Round(-eq.b / (2 * eq.a), 2);
                    M = Math.Round(-delta / (4 * eq.a), 2);

                    momentum = new Momentum(M, x);
                    if (!momentums.Contains(momentum) && x > eq.From && x < eq.To)
                        momentums.Add(momentum);
                }
            }

            return momentums;
        }

        public static List<Effect> GetImportantVerticalDeformations(Beam beam)
        {
            List<MomentumEquation> eqs = GetMomentumEquations(beam);
            if (eqs == null) return null;

            List<Effect> effects = new List<Effect>();
            
            foreach(MomentumEquation eq in GetMomentumEquations(beam))
            {
                Effect effect;

                //effects on the nodes
                double defFrom = GetDeformationFuncEI(beam, eq.From, Direction.Vertical);
                double defTo = GetDeformationFuncEI(beam, eq.To, Direction.Vertical);

                //adding node effects
                effect = new Effect();
                effect.VerticalDeformation = defFrom;
                effect.X = eq.From;
                if (!effects.Contains(effect))
                    effects.Add(effect);

                effect = new Effect();
                effect.VerticalDeformation = defTo;
                effect.X = eq.To;
                if (!effects.Contains(effect))
                    effects.Add(effect);

                //adding maximum effects
                double maxDef = defFrom;
                double x = eq.From;

                for (float f = (float) eq.From; f <= eq.To; f += 0.07f)
                {
                    double def = GetDeformationFuncEI(beam, f, Direction.Vertical);

                    if (Math.Sqrt(def * def) > Math.Sqrt(maxDef * maxDef))
                    {
                        maxDef = def;
                        x = f;
                    }
                }

                effect = new Effect();
                effect.VerticalDeformation = maxDef;
                effect.X = x;
                if (!effects.Contains(effect))
                    effects.Add(effect);
            }

            return effects;
        }

        public static List<Effect> GetImportantRotations(Beam beam)
        {
            List<MomentumEquation> eqs = GetMomentumEquations(beam);
            if (eqs == null) return null;

            List<Effect> effects = new List<Effect>();

            foreach (MomentumEquation eq in GetMomentumEquations(beam))
            {
                Effect effect;

                //effects on the nodes
                double rotFrom = GetRotationFuncEI(beam, eq.From);
                double rotTo = GetRotationFuncEI(beam, eq.To);

                //adding node effects
                effect = new Effect();
                effect.Rotation = rotFrom;
                effect.X = eq.From;
                if (!effects.Contains(effect))
                    effects.Add(effect);

                effect = new Effect();
                effect.Rotation = rotTo;
                effect.X = eq.To;
                if (!effects.Contains(effect))
                    effects.Add(effect);

                //adding maximum effects
                double maxRot = rotFrom;
                double x = eq.From;

                for (float f = (float)eq.From; f <= eq.To; f += 0.07f)
                {
                    double rot = GetRotationFuncEI(beam, f);

                    if (Math.Sqrt(rot * rot) > Math.Sqrt(maxRot * maxRot))
                    {
                        maxRot = rot;
                        x = f;
                    }
                }

                effect = new Effect();
                effect.Rotation = maxRot;
                effect.X = x;
                if (!effects.Contains(effect))
                    effects.Add(effect);
            }

            return effects;
        }

        public static bool AreSupportedByTheSameColumn(Beam b1, Beam b2)
        {
            List<Column> columns = b1.Structures
                .Where(s => s is Column)
                .Select(s => s as Column)
                .ToList();

            foreach (Column c in columns)
            {
                if (b1.Intersects(c) && b2.Intersects(c))
                {
                    if (b1.GetIntersectionPoint(c).Equals(b2.GetIntersectionPoint(c)))
                        return true;
                }
            }

            return false;
        }

        public static Beam GetMostUnfavorable(Beam b1, Beam b2)
        {
            double b1_def = 0;
            double b2_def = 0;

            Beam b1_ = b1.Clone();
            Beam b2_ = b2.Clone();

            IsoPosition intersection = b1_.GetIntersectionPoint(b2_);

            b1_.PointLoads.RemoveAll(p => p.Position.Equals(intersection));
            b2_.PointLoads.RemoveAll(p => p.Position.Equals(intersection));
            b1_.Supports.RemoveAll(s => s.Position.Equals(intersection));
            b2_.Supports.RemoveAll(s => s.Position.Equals(intersection));

            //Testing b1_def
            b1_.ComputeReactions();

            double x = SideViewPosition.FromIsoPosition(intersection, b1_).X;
            b1_def = GetDeformationFuncEI(b1_, x, Direction.Vertical);
            if(b1.E != 0 && b1.I != 0)
                b1_def = b1_def / (b1.E * b1.I);

            //testing b2_def
            b2_.ComputeReactions();

            x = SideViewPosition.FromIsoPosition(intersection, b2_).X;
            b2_def = GetDeformationFuncEI(b2_, x, Direction.Vertical);
            if (b2.E != 0 && b2.I != 0)
                b2_def = b2_def / (b2.E * b2.I);

            if (b1_def < b2_def)
                return b1;
            else
                return b2;
        }
    }
}
